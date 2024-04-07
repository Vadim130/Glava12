public class Task12
{
    // ПЛОХОЙ КОД!!
    static int ParallelSum(IEnumerable<int> values)
    {
        int result = 0;
        Parallel.ForEach(source: values,
        localInit: () => 0,
        body: (item, state, localValue) => localValue + item,
        localFinally: localValue => { result += localValue; });
        return result;
    }
    public static void Main(string[] args)
    {
        const int count = 10000;
        int s = ParallelSum(Enumerable.Range(1,count));
        Console.WriteLine("Calculated {0}, real {1}", s, (1+count)*count/2);
    }

}