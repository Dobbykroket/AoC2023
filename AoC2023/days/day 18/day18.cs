using AoC2023.tools;

namespace AoC2023.days.day_18;

public class Day18: Day
{
    public Day18()
    {
        this.Directory = "day 18";
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