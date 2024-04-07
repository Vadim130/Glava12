class MyClass
{
    private readonly ManualResetEventSlim _initialized =
    new ManualResetEventSlim();
    private int _value;
    public int WaitForInitialization()
    {
        _initialized.Wait();
        return _value;
    }
    public void InitializeFromAnotherThread()
    {
        _value = 13;
        _initialized.Set();
    }
 }
public class Task123
{
    public static void Main(string[] args)
    {
        MyClass c = new MyClass();
        Thread t = new Thread(c.InitializeFromAnotherThread);
        t.Start();
        Console.WriteLine(c.WaitForInitialization());
    }
}