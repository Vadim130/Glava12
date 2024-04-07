public class Task12
{
    // ПЛОХОЙ КОД!!
    static async Task<int> SimpleParallelismAsync()
    {
        int value = 0;
        Task task1 = Task.Run(() => { value = value + 1; });
        Task task2 = Task.Run(() => { value = value + 1; });
        Task task3 = Task.Run(() => { value = value + 1; });
        await Task.WhenAll(task1, task2, task3);
        return value;
    }
    public static async Task Main(string[] args)
    {
        int v = await SimpleParallelismAsync();
        Console.WriteLine(v);
    }

}