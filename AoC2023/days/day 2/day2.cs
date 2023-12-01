using AoC2023.tools;

namespace AoC2023.days.day_2;

public class Day2: Day
{
    public Day2()
    {
        this.Directory = "day 2";
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