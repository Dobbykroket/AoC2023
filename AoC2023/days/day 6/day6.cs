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
        int product = 1;
        int[] times = data.ReadLine().Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int[] records = data.ReadLine().Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int[] results = new int[times.Length];
        for (int i = 0; i < times.Length; i++)
        {
            for (int j = 0; j < (times[i] * 0.5); j++)
            {
                if (j * (times[i] - j) > records[i])
                {
                    results[i] = (times[i] - (2 * j) + 1);
                    break;
                }
            }
        }

        foreach (int result in results)
        {
            product *= result;
        }
        Console.WriteLine(product);
    }
}