using System.Threading.Tasks.Dataflow;

public class MyClass
{
    async Task<string[]> DownloadUrlsAsync(HttpClient client,
     IEnumerable<string> urls)
    {
        using var semaphore = new SemaphoreSlim(10);
        Task<string>[] tasks = urls.Select(async url =>
        {
            await semaphore.WaitAsync();
            try
            {
                return await client.GetStringAsync(url);
            }
            finally
            {
                semaphore.Release();
            }
        }).ToArray();
        return await Task.WhenAll(tasks);
    }

}