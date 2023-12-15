using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_15;

public class Day15: Day
{
    private struct Lens
    {
        public string Label;
        public int Strength;

        public Lens(string label, int strength)
        {
            Label = label;
            Strength = strength;
        }

        public override bool Equals(object? obj) => obj is Lens other && this.Equals(other);

        public bool Equals(Lens other) => Label.Equals(other.Label);
    }
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
        List<Lens>[] boxes = new List<Lens>[256];
        for (int i = 0; i < 256; i++)
        {
            boxes[i] = new List<Lens>();
        }
        foreach (string step in steps)
        {
            string label;
            int strength;

            if (step[^1].Equals('-'))
            {
                label = step.Substring(0, step.Length - 1);
                List<Lens> box = boxes[Hash(label)];
                box.Remove(new Lens(label, -1));
            }

            if (Char.IsDigit(step[^1]))
            {
                label = step.Substring(0, step.Length - 2);
                strength = int.Parse(step[^1].ToString());
                List<Lens> box = boxes[Hash(label)];
                Lens lens = new Lens(label, strength);
                if (box.Contains(lens))
                {
                    int index = box.IndexOf(lens);
                    box[index] = lens;
                }
                else
                {
                    box.Add(lens);
                }
            }

        }

        long power = 0;

        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                power += (i + 1) * (j + 1) * boxes[i][j].Strength;
            }
        }

        Console.WriteLine(power);
    }
}