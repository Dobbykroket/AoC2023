using AoC2023.tools;

namespace AoC2023.days.day_17;

public class Day17: Day
{
    public Day17()
    {
        this.Directory = "day 17";
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