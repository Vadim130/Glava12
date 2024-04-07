using Nito.AsyncEx;

class MyClass
{
    private readonly AsyncManualResetEvent _connected =
    new AsyncManualResetEvent();
    public async Task WaitForConnectedAsync()
    {
        await _connected.WaitAsync();
    }
    public void ConnectedChanged(bool connected)
    {
        if (connected)
            _connected.Set();
        else
            _connected.Reset();
    }
}
public class Task1242
{
    public static async Task Main(string[] args)
    {
        MyClass myClass = new MyClass();
        Task task = myClass.WaitForConnectedAsync();
        myClass.ConnectedChanged(true);
        await task;
    }
}