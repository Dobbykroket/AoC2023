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
        int sum = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            int winningcount = 0;
            string[] carddata = line.Split(":");
            string[] numbers = carddata[1].Split("|");
            List<string> winningnumbers = numbers[0].Trim().Split(" ").ToList();
            List<string> yournumbers = numbers[1].Trim().Split(" ").ToList();
            
            while (winningnumbers.Contains(""))
            {
                winningnumbers.Remove("");
            }
            while (yournumbers.Contains(""))
            {
                yournumbers.Remove("");
            }
            foreach (string number in yournumbers)
            {
                if (winningnumbers.Contains(number))
                {
                    winningcount++;
                }
            }

            if (winningcount > 0)
            {
                sum += (int) Math.Pow(2, winningcount - 1);
            }
        }
        Console.WriteLine(sum);
    }
}