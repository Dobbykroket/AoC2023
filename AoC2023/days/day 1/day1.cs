using AoC2023.tools;

namespace AoC2023.days.day_1;

public class Day1: Day
{
    public Day1()
    {
        this.Directory = "day 1";
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