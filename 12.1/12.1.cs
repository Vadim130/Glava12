
class MyClass
{
    // Блокировка защищает поле _value.
    private readonly object _mutex = new object();
    private int _value;
    public void Increment()
    {
        lock (_mutex)
        {
            _value = _value + 1;
        }
    }
    public int Stask()
    {
        Parallel.For(0, 1000, (x) => { Increment(); });
        return _value;
    }
}
public class Task121
{
    public static void Main(string[] args)
    {
        MyClass c = new MyClass();
        Console.WriteLine(c.Stask());
    }
}