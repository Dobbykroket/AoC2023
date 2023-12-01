using AoC2023.tools;

namespace AoC2023.days.day_25;

public class Day25: Day
{
    public Day25()
    {
        this.Directory = "day 25";
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