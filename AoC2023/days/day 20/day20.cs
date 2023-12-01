using AoC2023.tools;

namespace AoC2023.days.day_20;

public class Day20: Day
{
    public Day20()
    {
        this.Directory = "day 20";
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