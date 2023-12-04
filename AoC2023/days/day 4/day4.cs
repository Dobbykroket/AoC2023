using AoC2023.tools;

namespace AoC2023.days.day_4;

public class Day4: Day
{
    private class scratchcard
    {
        public int cardnumber;
        public List<string> winningnumbers;
        public List<string> yournumbers;
        public int winningcount = 0;
        public int timesscratched = 1;

        public scratchcard(int cardnumber, List<string> winningnumbers, List<string> yournumbers)
        {
            this.cardnumber = cardnumber;
            this.winningnumbers = winningnumbers;
            this.yournumbers = yournumbers;
        }

        public void scratch()
        {
            foreach (string number in yournumbers)
            {
                if (winningnumbers.Contains(number))
                {
                    winningcount++;
                }
            }
        }
    }
    public Day4()
    {
        this.Directory = "day 4";
    }
    public override void Run()
    {
        List<scratchcard> cards = new List<scratchcard>(); 
        int sum = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
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

            cards.Add(new scratchcard(int.Parse(carddata[0].Substring(4, carddata[0].Length - 4)), winningnumbers, yournumbers));

        }

        foreach (scratchcard card in cards)
        {
            card.scratch();
            for (int i = card.cardnumber; i < (card.cardnumber + card.winningcount) && i < cards.Count; i++)
            {
                cards[i].timesscratched += card.timesscratched;
            }

            sum += card.timesscratched;
        }
        
        Console.WriteLine(sum);
    }
}