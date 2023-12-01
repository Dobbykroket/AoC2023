using AoC2023.tools;

namespace AoC2023.days.day_0;

public class Day0: Day
{
    public Day0()
    {
        this.Directory = "day 0";
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