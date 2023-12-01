using AoC2023.tools;

namespace AoC2023.days.day_9;

public class Day9: Day
{
    public Day9()
    {
        this.Directory = "day 9";
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