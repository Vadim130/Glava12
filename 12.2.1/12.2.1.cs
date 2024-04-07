class MyClass
{
    // Блокировка защищает поле _value.
    private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);
    private int _value;
    public MyClass(int value)
    {
        _value = value;
    }
    public async Task DelayAndIncrementAsync()
    {
        await _mutex.WaitAsync();
        try
        {
            int oldValue = _value;
            await Task.Delay(TimeSpan.FromSeconds(oldValue));
            _value = oldValue + 1;
        }
        finally
        {
            _mutex.Release();
        }
    }
}
class Task1221
{
    public async static Task Main(string[] args)
    {
        MyClass c = new MyClass(2);
        await c.DelayAndIncrementAsync();
    }
}