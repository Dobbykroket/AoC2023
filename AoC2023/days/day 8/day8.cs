using AoC2023.tools;

namespace AoC2023.days.day_8;

public class Day8: Day
{
    public Day8()
    {
        this.Directory = "day 8";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        string? line;

        int stepcount = 0;

        string instructions = data.ReadLine();
        data.ReadLine(); // clear blank line
        
        Dictionary<string, string[]> nodes = new Dictionary<string, string[]>();

        while ((line = data.ReadLine()) != null)
        {
            string[] input = line.Split("=", StringSplitOptions.TrimEntries);
            nodes.Add(input[0], input[1].Substring(1, input[1].Length - 2).Split(",", StringSplitOptions.TrimEntries));
        }

        int index = 0;
        string currentnode = "AAA";

        while (!currentnode.Equals("ZZZ"))
        {
            stepcount++;
            index %= instructions.Length;

            char currentinstruction = instructions[index];
            
            switch (currentinstruction)
            {
                case 'L':
                {
                    currentnode = nodes.GetValueOrDefault(currentnode)[0];
                    break;
                }
                case 'R':
                {
                    currentnode = nodes.GetValueOrDefault(currentnode)[1];
                    break;
                }
            }

            index++;
        }
        
        Console.WriteLine(stepcount);
    }
}