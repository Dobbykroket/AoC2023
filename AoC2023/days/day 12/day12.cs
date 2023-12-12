using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_12;

public class Day12: Day
{
    private bool validPlacement(string corrupteddata, int backupdata)
    {
        if (corrupteddata.Length < backupdata) // snippet too short to fit block
        {
            return false;
        }

        if (corrupteddata.Length == backupdata) // sippet exactly fits block
        {
            return true;
        }

        if (corrupteddata[backupdata].Equals('#')) // check if this doesn't put a confirmed spot as separator
        {
            return false;
        }

        return true; // no further issues
    }
    
    public Day12()
    {
        this.Directory = "day 12";
    }
    public override void Run()
    {
        int totalpossibillities = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            int possibillities = 0;
            string corrupteddata = line.Split(" ")[0];
            int[] backupdata = line.Split(" ")[1].Split(",").Select(int.Parse).ToArray();
            int blanksquares = corrupteddata.Length - (backupdata.Length - 1) - backupdata.Sum();

            int[] blocks = new int[blanksquares + backupdata.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = i;
            }
            foreach (var combination in blocks.Combinations(backupdata.Length))
            {
                int[] picked = new int[blocks.Length];
                Array.Fill(picked, 0);

                foreach (int pickedindex in combination)
                {
                    picked[pickedindex] = 1;
                }

                int backupindex = 0;
                StringBuilder possiblelineBuilder = new StringBuilder();
                foreach (int val in picked)
                {
                    if (val == 0)
                    {
                        possiblelineBuilder.Append(".");
                    }

                    if (val == 1)
                    {
                        char[] block = new char[backupdata[backupindex]];
                        Array.Fill(block, '#');
                        backupindex++;
                        possiblelineBuilder.Append(block);
                        if (backupindex != backupdata.Length)
                        {
                            possiblelineBuilder.Append(".");
                        }
                    }
                }
                string possibleline = possiblelineBuilder.ToString();
                bool possible = true;
                for (int i = 0; i < possibleline.Length; i++)
                {
                    if (!(corrupteddata[i].Equals('?') || corrupteddata[i].Equals(possibleline[i])))
                    {
                        possible = false;
                        break;
                    }
                }
                if (possible)
                {
                    possibillities++;
                }
            }
            
            

            totalpossibillities += possibillities;
        }
        Console.WriteLine(totalpossibillities);
    }
}