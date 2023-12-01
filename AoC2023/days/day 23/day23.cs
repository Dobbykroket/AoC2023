using AoC2023.tools;

namespace AoC2023.days.day_23;

public class Day23: Day
{
    public Day23()
    {
        this.Directory = "day 23";
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