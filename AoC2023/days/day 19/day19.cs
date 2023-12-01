using AoC2023.tools;

namespace AoC2023.days.day_19;

public class Day19: Day
{
    public Day19()
    {
        this.Directory = "day 19";
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