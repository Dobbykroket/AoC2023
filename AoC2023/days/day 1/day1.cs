using AoC2023.tools;

namespace AoC2023.days.day_1;

public class Day1: Day
{
    public Day1()
    {
        this.Directory = "day 1";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        string? line;
        int sum = 0;
        while ((line = data.ReadLine()) != null)
        {
            char? firstint = null;
            char? lastint = null;
            foreach (char c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    if (firstint is null)
                    {
                        firstint = c;
                    }

                    lastint = c;
                }
            }

            firstint = firstint ?? '0';
            lastint = lastint ?? firstint;
            sum += int.Parse(string.Concat(firstint, lastint));
        }

        Console.WriteLine(sum);
    }
}