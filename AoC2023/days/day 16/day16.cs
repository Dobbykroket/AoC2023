using AoC2023.tools;

namespace AoC2023.days.day_16;

public class Day16: Day
{
    public Day16()
    {
        this.Directory = "day 16";
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