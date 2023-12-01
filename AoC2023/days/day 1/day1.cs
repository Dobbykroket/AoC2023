using AoC2023.tools;

namespace AoC2023.days.day_1;

public class Day1: Day
{
    internal struct digit
    {
        public string name;
        public string character;

        public digit(string name, string character)
        {
            this.name = name;
            this.character = character;
        }
    }
    digit[] digits = {  new digit("one", "1"), new digit("two", "2"), 
                        new digit("three", "3"), new digit("four", "4"), 
                        new digit("five", "5"), new digit("six", "6"), 
                        new digit("seven", "7"), new digit("eight", "8"),
                        new digit("nine", "9")};
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
            line = replaceDigits(line);
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

            firstint ??= '0';
            lastint ??= firstint;
            sum += int.Parse(string.Concat(firstint, lastint));
        }

        Console.WriteLine(sum);
    }

    private String replaceDigits(String input)
    {
        foreach (digit digit in digits)
        {
            if (input.Contains(digit.name))
            {
                string[] collection = input.Split(digit.name);
                for (int i = 0; i < collection.Length - 1; i++)
                {
                    collection[i] += digit.name + digit.character + digit.name;
                }

                input = String.Join("", collection);
            }
        }

        return input;
    }
}