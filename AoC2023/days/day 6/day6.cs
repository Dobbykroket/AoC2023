using AoC2023.tools;

namespace AoC2023.days.day_6;

public class Day6: Day
{
    public Day6()
    {
        this.Directory = "day 6";
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