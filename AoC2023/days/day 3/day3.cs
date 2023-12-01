using AoC2023.tools;

namespace AoC2023.days.day_3;

public class Day3: Day
{
    public Day3()
    {
        this.Directory = "day 3";
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