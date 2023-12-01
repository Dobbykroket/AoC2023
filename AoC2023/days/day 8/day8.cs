using AoC2023.tools;

namespace AoC2023.days.day_8;

public class Day8: Day
{
    public Day8()
    {
        this.Directory = "day 8";
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