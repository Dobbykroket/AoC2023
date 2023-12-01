using AoC2023.tools;

namespace AoC2023.days.day_22;

public class Day22: Day
{
    public Day22()
    {
        this.Directory = "day 22";
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