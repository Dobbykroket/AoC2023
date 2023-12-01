using AoC2023.tools;

namespace AoC2023.days.day_21;

public class Day21: Day
{
    public Day21()
    {
        this.Directory = "day 21";
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