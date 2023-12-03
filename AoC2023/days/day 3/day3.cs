using AoC2023.tools;

namespace AoC2023.days.day_3;

public class Day3: Day
{
    string? topperline = null;
    string? topline = null;
    string? middleline = null;
    string? bottomline;
    private struct location
    {
        public string line;
        public int index;

        public location(string line, int index)
        {
            this.line = line;
            this.index = index;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            location otherlocation = (location)obj;
            if (line.Equals(otherlocation.line) && index.Equals(otherlocation.index))
            {
                return true;
            }

            return false;
        }
    }
    private char[] validchars = { '*' };
    public Day3()
    {
        this.Directory = "day 3";
    }
    public override void Run()
    {
        int sum = 0;
        int count = 0;
        StreamReader data = LoadData();
        bool dataEmpty = false;
        while (!dataEmpty)
        {
            bottomline = data.ReadLine();
            if (topline is not null)
            {
                string currentint = "";
                bool possiblegear = false;
                List<location> gearlocations = new List<location>();
                for (int i = 0; i < topline.Length; i++)
                {
                    char c = char.Parse(topline.Substring(i, 1));
                    if (char.IsDigit(c))
                    {
                        
                        currentint += c;
                        {
                            if (topperline is not null && i < topperline.Length - 1 && validchars.Contains(char.Parse(topperline.Substring(i + 1, 1))))
                            {
                                possiblegear = true;
                                gearlocations.Add(new location("topper", i + 1));
                            }
                            if (i < topline.Length - 1 && validchars.Contains(char.Parse(topline.Substring(i + 1, 1))))
                            {
                                possiblegear = true;
                                gearlocations.Add(new location("top", i + 1));
                            }
                            if (i > 0 && validchars.Contains(char.Parse(topline.Substring(i - 1, 1))))
                            {
                                possiblegear = true;
                                gearlocations.Add(new location("top", i - 1));
                            }
                            if (middleline is not null)
                            {
                                if (i > 0 && validchars.Contains(char.Parse(middleline.Substring(i - 1, 1))))
                                {
                                    possiblegear = true;
                                    gearlocations.Add(new location("middle", i - 1));
                                } 
                                if (validchars.Contains(char.Parse(middleline.Substring(i, 1))))
                                {
                                    possiblegear = true;
                                    gearlocations.Add(new location("middle", i));
                                }
                                if (i < middleline.Length - 1 &&  validchars.Contains(char.Parse(middleline.Substring(i + 1, 1))))
                                {
                                    possiblegear = true;
                                    gearlocations.Add(new location("middle", i + 1));
                                }
                            }
                        }
                    }
                    if (!char.IsDigit(c) || (i == topline.Length - 1))
                    {
                        if (currentint != "" && possiblegear)
                        {
                            List<location> dedupedgears = gearlocations.Distinct().ToList();
                            foreach (location gear in dedupedgears)
                            {
                                if (!(gear.line == "topper" && gear.index != i && i != topline.Length - 1))
                                {
                                    string secondint = exploregear(gear, currentint);
                                    if (secondint != "")
                                    {
                                        sum += int.Parse(currentint) * int.Parse(secondint);
                                        count++;
                                    }
                                }
                            }
                        }

                        currentint = "";
                        possiblegear = false;
                        gearlocations.Clear();
                    }
                }
            }

            topperline = topline;
            topline = middleline;
            middleline = bottomline;
            dataEmpty = (middleline is null);
        }
        Console.WriteLine(sum);
    }

    private string forwardsearch(string line, int index)
    {
        char h = char.Parse(line.Substring(index, 1));
        if (h.Equals('.') && index == 0)
        {
            index++;
            h = char.Parse(line.Substring(index, 1));
        }
        string secondint = "";
        while (char.IsDigit(h) && index < line.Length)
        {
            secondint += h;
            index++;
            if (index < line.Length)
            {
                h = char.Parse(line.Substring(index, 1));
            }
        }

        return secondint;
    }

    private string reversesearch(string line, int index)
    {
        if (line is null)
        {
            return "";
        }
        char h = char.Parse(line.Substring(index, 1));
        if (!char.IsDigit(h))
        {
            return "";
        }
        while (char.IsDigit(h) && index >= 1)
        {
            index--;
            h = char.Parse(line.Substring(index, 1));
        }

        if (index == 0)
        {
            return forwardsearch(line, index);
        }
        return forwardsearch(line, index + 1);
    }

    private string exploregear(location gear, string currentint)
    {
        switch (gear.line)
        {
            case "topper":
            {
                return forwardsearch(topline, gear.index + 1);
            }
            case "top":
            {
                List<string> results = new List<string>();
                results.Add(forwardsearch(topline, gear.index + 1));
                results.Add(forwardsearch(middleline, gear.index + 1));
                results.Add(reversesearch(middleline, gear.index - 1));
                while (results.Contains(""))
                {
                    results.Remove("");
                }

                while (results.Contains(currentint))
                {
                    results.Remove(currentint);
                }

                if (results.Count == 0)
                {
                    return "";
                }
                if (results.TrueForAll(e => e.Equals(results[0])))
                {
                    return results[0];
                }
                return "";
            }
            case "middle":
            {
                List<string> results = new List<string>();
                results.Add(reversesearch(topline, gear.index + 1));
                results.Add(forwardsearch(middleline, gear.index + 1));
                results.Add(reversesearch(middleline, gear.index - 1));
                results.Add(reversesearch(bottomline, gear.index + 1));
                results.Add(reversesearch(bottomline, gear.index - 1));
                while (results.Contains(""))
                {
                    results.Remove("");
                }

                while (results.Contains(currentint))
                {
                    results.Remove(currentint);
                }

                if (results.Count == 0)
                {
                    return "";
                }
                if (results.TrueForAll(e => e.Equals(results[0])))
                {
                    return results[0];
                }
                return "";
                
            }
            default:
            {
                return "";
            }

        }
    }
}