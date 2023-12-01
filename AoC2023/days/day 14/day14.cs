using AoC2023.tools;

namespace AoC2023.days.day_14;

public class Day14: Day
{
    public Day14()
    {
        this.Directory = "day 14";
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