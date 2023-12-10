using System.Collections;

namespace AoC2023.tools;

public abstract class Day
{
    protected string? Directory;

    public void TimedRun()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Run();
        watch.Stop();
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }
    
    public abstract void Run();

    protected StreamReader LoadData()
    {
        FileInfo file = new FileInfo(String.Format("days/{0}/data.txt", Directory));
        StreamReader reader = new StreamReader(file.FullName);
        return reader;
    }

    protected StreamReader LoadExampleData()
    {
        FileInfo file = new FileInfo(String.Format("days/{0}/exampledata.txt", Directory));
        StreamReader reader = new StreamReader(file.FullName);
        return reader;
    }

    protected StreamReader LoadExample2Data()
    {
        FileInfo file = new FileInfo(String.Format("days/{0}/exampledata2.txt", Directory));
        StreamReader reader = new StreamReader(file.FullName);
        return reader;
    }

    protected StreamReader LoadExample3Data()
    {
        FileInfo file = new FileInfo(String.Format("days/{0}/exampledata3.txt", Directory));
        StreamReader reader = new StreamReader(file.FullName);
        return reader;
    }
}