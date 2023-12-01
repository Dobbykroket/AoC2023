using AoC2023.tools;

namespace AoC2023.days.day_4;

public class Day4: Day
{
    public Day4()
    {
        this.Directory = "day 4";
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