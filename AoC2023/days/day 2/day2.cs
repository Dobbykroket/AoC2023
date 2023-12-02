using System.Text.RegularExpressions;
using AoC2023.tools;

namespace AoC2023.days.day_2;

public class Day2: Day
{
    public Day2()
    {
        this.Directory = "day 2";
    }
    public override void Run()
    {
        int maxred = 12;
        int maxgreen = 13;
        int maxblue = 14;
        string redregex = "(\\d*) red";
        string blueregex = "(\\d*) blue";
        string greenregex = "(\\d*) green";
        
        int sum = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            int linemaxred = 0;
            int linemaxblue = 0;
            int linemaxgreen = 0;
            
            bool validGame = true;
            string[] content = line.Split(':');
            int gameID = int.Parse(content[0].Substring(5));
            string[] contentdata = content[1].Split(';');
            for (int i = 0; i < contentdata.Length; i++)
            {
                int redcount = 0;
                int bluecount = 0;
                int greencount = 0;

                var redmatches = Regex.Matches(contentdata[i], redregex);
                var bluematches = Regex.Matches(contentdata[i], blueregex);
                var greenmatches = Regex.Matches(contentdata[i], greenregex);

                if (redmatches.Count > 0 && redmatches[0].Groups.Count > 1)
                {
                    redcount = int.Parse(redmatches[0].Groups[1].Value);
                    if (redcount > linemaxred)
                    {
                        linemaxred = redcount;
                    }
                }

                if (bluematches.Count > 0 && bluematches[0].Groups.Count > 1)
                {
                    bluecount = int.Parse(bluematches[0].Groups[1].Value);
                    if (bluecount > linemaxblue)
                    {
                        linemaxblue = bluecount;
                    }
                }

                if (greenmatches.Count > 0 && greenmatches[0].Groups.Count > 1)
                {
                    greencount = int.Parse(greenmatches[0].Groups[1].Value);
                    if (greencount > linemaxgreen)
                    {
                        linemaxgreen = greencount;
                    }
                }

            }

            int power = linemaxred * linemaxblue * linemaxgreen;
            sum += power;

            /* part 1

            if (redcount > maxred || bluecount > maxblue || greencount > maxgreen)
            {
                validGame = false;
            }
        }

        if (validGame)
        {
            sum += gameID;
        }*/
        }
        
        Console.WriteLine(sum);
    }
}