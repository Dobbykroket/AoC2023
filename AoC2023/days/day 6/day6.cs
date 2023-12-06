using AoC2023.tools;

namespace AoC2023.days.day_6;

public class Day6: Day
{
    public Day6()
    {
        this.Directory = "day 6";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        long product = 1;
        long times = long.Parse(data.ReadLine().Split(":")[1].Replace(" ", string.Empty));
        long records = long.Parse(data.ReadLine().Split(":")[1].Replace(" ", string.Empty));
        long results = 0;
        for (long j = 0; j < (times * 0.5); j++)
        {
            if (j * (times - j) > records)
            {
                results = (times - (2 * j) + 1);
                break;
            }
        }
        Console.WriteLine(results);
    }
}