using AoC2023.tools;

namespace AoC2023.days.day_3;

public class Day3: Day
{
    private char[] validchars = { '-', '$', '%', '/', '*', '@', '=', '+', '&', '#'};
    public Day3()
    {
        this.Directory = "day 3";
    }
    public override void Run()
    {
        int sum = 0;
        StreamReader data = LoadData();
        string? topline = null;
        string? middleline = null;
        string? bottomline;
        bool dataEmpty = false;
        while (!dataEmpty)
        {
            bottomline = data.ReadLine();
            if (middleline is not null)
            {
                string currentint = "";
                bool partnumber = false;
                for (int i = 0; i < middleline.Length; i++)
                {
                    char c = char.Parse(middleline.Substring(i, 1));
                    if (char.IsDigit(c))
                    {
                        currentint += c;
                        if (!partnumber)
                        {
                            if ((i > 0 && validchars.Contains(char.Parse(middleline.Substring(i - 1, 1)))) ||
                                (i < middleline.Length - 1 &&
                                 validchars.Contains(char.Parse(middleline.Substring(i + 1, 1)))))
                            {
                                partnumber = true;
                            }

                            if (topline is not null && !partnumber)
                            {
                                if ((i > 0 && validchars.Contains(char.Parse(topline.Substring(i - 1, 1)))) ||
                                    (validchars.Contains(char.Parse(topline.Substring(i, 1)))) ||
                                    (i < topline.Length - 1 &&
                                     validchars.Contains(char.Parse(topline.Substring(i + 1, 1)))))
                                {
                                    partnumber = true;
                                }
                            }

                            if (bottomline is not null && !partnumber)
                            {
                                if ((i > 0 && validchars.Contains(char.Parse(bottomline.Substring(i - 1, 1)))) ||
                                    (validchars.Contains(char.Parse(bottomline.Substring(i, 1)))) ||
                                    (i < bottomline.Length - 1 &&
                                     validchars.Contains(char.Parse(bottomline.Substring(i + 1, 1)))))
                                {
                                    partnumber = true;
                                }
                            }
                        }
                    }
                    if (!char.IsDigit(c) || (i == middleline.Length - 1))
                    {
                        if (currentint != "")
                        {
                            if (partnumber)
                            {
                                sum += int.Parse(currentint);
                            }

                            currentint = "";
                            partnumber = false;
                        }
                    }
                }
            }

            topline = middleline;
            middleline = bottomline;
            dataEmpty = (bottomline is null);
        }
        Console.WriteLine(sum);
    }
}