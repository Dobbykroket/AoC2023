using AoC2023.tools;

namespace AoC2023.days.day_11;

public class Day11: Day
{
    public Day11()
    {
        this.Directory = "day 11";
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