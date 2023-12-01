using System.Collections;

namespace AoC2023.tools;

public abstract class Day
{
    protected string? Directory;
    
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
}