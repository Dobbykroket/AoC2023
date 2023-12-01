using AoC2023.tools;

namespace AoC2023.days.day_10;

public class Day10: Day
{
    public Day10()
    {
        this.Directory = "day 10";
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