using AoC2023.tools;

namespace AoC2023.days.day_7;

public class Day7: Day
{
    public Day7()
    {
        this.Directory = "day 7";
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