using System.Diagnostics;

public class Task12
{
    public static async Task Main(string[] args)
    {
        Task myTask = MyMethodAsync();
        await myTask;
    }
    static async Task MyMethodAsync()
    {
        int value = 10;
        await Task.Delay(TimeSpan.FromSeconds(1));
        value = value + 1;
        await Task.Delay(TimeSpan.FromSeconds(1));
        value = value - 1;
        await Task.Delay(TimeSpan.FromSeconds(1));
        Trace.WriteLine(value);
    }
}