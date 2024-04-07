using System.Diagnostics;
public class ModVal
{
    private int value = 0;
    async Task ModifyValueAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        value = value + 1;
    }
    // ВНИМАНИЕ: может требовать синхронизации; см. ниже.
    public async Task<int> ModifyValueConcurrentlyAsync()
    {
        // Start three concurrent modifications.
        Task task1 = ModifyValueAsync();
        Task task2 = ModifyValueAsync();
        Task task3 = ModifyValueAsync();
        await Task.WhenAll(task1, task2, task3);
        return value;
    }
}
public class Task12
{
    public static async Task Main(string[] args)
    {
        ModVal mv = new ModVal();
        int v = await mv.ModifyValueConcurrentlyAsync();
        Console.WriteLine(v);
    }
    
}


