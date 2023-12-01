using AoC2023.tools;

namespace AoC2023.days.day_5;

public class Day5: Day
{
    public Day5()
    {
        this.Directory = "day 5";
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