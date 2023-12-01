using AoC2023.tools;

namespace AoC2023.days.day_13;

public class Day13: Day
{
    public Day13()
    {
        this.Directory = "day 13";
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