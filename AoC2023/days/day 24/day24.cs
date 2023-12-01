using AoC2023.tools;

namespace AoC2023.days.day_24;

public class Day24: Day
{
    public Day24()
    {
        this.Directory = "day 24";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}