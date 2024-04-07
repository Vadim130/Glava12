using System.Diagnostics;

public class Task12
{
    static void IndependentParallelism(IEnumerable<int> values)
    {
        Parallel.ForEach(values, item => Trace.WriteLine(item));
    }
    public static void Main(string[] args)
    {
        IndependentParallelism(new int [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
    }

}