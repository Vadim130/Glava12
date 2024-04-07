using System.Threading;
using System.Threading.Tasks.Dataflow;

public class Matrix
{
    float[,] els = new float[2, 2];
    public Matrix(float[,] e)
    {
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                els[i, j] = e[i, j];
    }
    public void Rotate(float degrees)
    {
        Matrix old = new Matrix(els);
        float angle = degrees * (float)Math.PI / 180;
        float c = (float)Math.Cos(angle), s = (float)Math.Sin(angle);
        /*
            *  ( a00' a01')   ( c  -s )  ( a00 a01)
            *  ( a10' a11') = ( s   c )* ( a10 a11)
            */
        els[0, 0] = old.els[0, 0] * c - old.els[1, 0] * s;
        els[0, 1] = old.els[0, 1] * c - old.els[1, 1] * s;
        els[1, 0] = old.els[0, 0] * s + old.els[1, 0] * c;
        els[1, 1] = old.els[0, 1] * s + old.els[1, 1] * c;
    }
    public float det()
    {
        return els[0, 0] * els[1, 1] - els[1, 0] * els[0, 1];
    }
    public bool IsInvertible
    {
        get { return det() != 0; }
    }

    public void Invert()
    {
        float d = det();
        Matrix old = new Matrix(els);
        els[0, 0] = old.els[1, 1] / d;
        els[1, 1] = old.els[0, 0] / d;
        els[0, 1] = -old.els[0, 1] / d;
        els[1, 0] = -old.els[1, 0] / d;
    }
    public override String ToString()
    {
        return "{ {" + els[0, 0] + ", " + els[0, 1] + "}, {" + els[1, 0] + ", " + els[1, 1] + "} }";
    }
}
public class Task1251
{
    
    static IPropagatorBlock<int, int> DataflowMultiplyBy2()
    {
        var options = new ExecutionDataflowBlockOptions
        {
            MaxDegreeOfParallelism = 10
        };
        return new TransformBlock<int, int>(data => data * 2, options);
    }
    // Использование Parallel LINQ (PLINQ)
    static IEnumerable<int> ParallelMultiplyBy2(IEnumerable<int> values)
    {
        return values.AsParallel()
        .WithDegreeOfParallelism(10)
        .Select(item => item * 2);
    }
    // Использование класса Parallel
    static void ParallelRotateMatrices(IEnumerable<Matrix> matrices, float degrees)
    {
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 10
        };
        Parallel.ForEach(matrices, options, matrix => matrix.Rotate(degrees));
    }
    public static void Main(string[] args)
    {
        var m2 = DataflowMultiplyBy2();
        for (int i = 0; i < 100; i++)
            m2.Post(i);
        for (int i = 0; i < 100; i++)
            Console.WriteLine(m2.Receive());
        foreach (int v in ParallelMultiplyBy2(Enumerable.Range(1, 100)))
            Console.WriteLine(v);
        Matrix[] matrices = new Matrix[180];
        
        for (int i = 0; i < 180; i++)
        {
            matrices[i] = new Matrix(new float[,] { { 1, 0 }, { 0, 1 } });
            matrices[i].Rotate(i);
        }
        Console.WriteLine("Matrix initialization done");
        ParallelRotateMatrices(matrices, 180);
        foreach (var v in matrices)
            Console.WriteLine(v);
        Console.WriteLine("Matrix transformation done");

    }
}