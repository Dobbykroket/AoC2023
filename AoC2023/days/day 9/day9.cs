using AoC2023.tools;

namespace AoC2023.days.day_9;

public class Day9: Day
{
    public Day9()
    {
        this.Directory = "day 9";
    }

    public int[] FindDifferences(int[] nums)
    {
        int[] results = new int[nums.Length - 1];

        for (int i = 0; i < results.Length; i++)
        {
            results[i] = nums[i + 1] - nums[i];
        }

        return results;
    }

    public int[] AddFirstValue(int[] nums)
    {
        List<int> vals = new List<int>();
        vals.AddRange(nums);
        if (nums.All(num => num == 0))
        {
            vals.Insert(0, 0);
            return vals.ToArray();
        }

        int[] diffs = AddFirstValue(FindDifferences(nums));

        vals.Insert(0, vals[0] - diffs[0]);
        return vals.ToArray();
    }

    public int[] AddNextValue(int[] nums)
    {
        List<int> vals = new List<int>();
        vals.AddRange(nums);
        if (nums.All(num => num == 0))
        {
            vals.Add(0);
            return vals.ToArray();
        }

        int[] diffs = AddNextValue(FindDifferences(nums));

        vals.Add(vals[^1] + diffs[^1]);
        return vals.ToArray();
    }
    
    public override void Run()
    {
        int sum = 0;
        
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            int[] nums = line.Split(" ").Select(s => int.Parse(s)).ToArray();
            int extrapolated = AddFirstValue(nums)[0];

            Console.WriteLine(extrapolated);
            sum += extrapolated;
        }
        Console.WriteLine(sum);
    }
}