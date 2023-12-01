using AoC2023.tools;

namespace AoC2023.days.day_15;

public class Day15: Day
{
    public Day15()
    {
        this.Directory = "day 15";
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