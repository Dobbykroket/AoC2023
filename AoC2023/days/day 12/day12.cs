using AoC2023.tools;

namespace AoC2023.days.day_12;

public class Day12: Day
{
    public Day12()
    {
        this.Directory = "day 12";
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