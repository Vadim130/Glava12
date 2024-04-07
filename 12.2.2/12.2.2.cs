using Microsoft.EntityFrameworkCore.Internal;
using Nito.AsyncEx;
using System.Threading;

class MyClass
{
    public MyClass(int value)
    {
        _value = value;
    }
    // Блокировка защищает поле _value.
    private readonly AsyncLock _mutex = new AsyncLock();
    private int _value;
    public async Task DelayAndIncrementAsync()
    {
        using (await _mutex.LockAsync())
        {
            int oldValue = _value;
            await Task.Delay(TimeSpan.FromSeconds(oldValue));
            _value = oldValue + 1;
        }
    }
}
class Task1222
{
    public async static Task Main(string[] args)
    {
        MyClass c = new MyClass(2);
        await c.DelayAndIncrementAsync();
    }
}