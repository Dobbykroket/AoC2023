using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_15;

public class Day15: Day
{
    private int Hash(String input)
    {
        int currentvalue = 0;
        foreach (char c in input)
        {
            currentvalue += (int)c; //  dirty but works as long as input is ASCII-only
            currentvalue *= 17;
            currentvalue %= 256;
        }

        return currentvalue;
    }
    public Day15()
    {
        this.Directory = "day 15";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        string line = data.ReadLine()!;
        string[] steps = line.Split(",");
        int sum = 0;
        foreach (string step in steps)
        {
            sum += Hash(step);
        }
        
        Console.WriteLine(sum);
    }
}