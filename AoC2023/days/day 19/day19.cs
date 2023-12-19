using System.Diagnostics;
using AoC2023.tools;
using Microsoft.VisualBasic;

namespace AoC2023.days.day_19;

public class Day19: Day
{
    private struct Part
    {
        public int x;
        public int m;
        public int a;
        public int s;
    }
    public Day19()
    {
        this.Directory = "day 19";
    }

    private class range
    {
        public int xmin;
        public int xmax;

        public int mmin;
        public int mmax;

        public int amin;
        public int amax;

        public int smin;
        public int smax;

        public bool? accepted;

        public range(int xmin, int xmax, int mmin, int mmax, int amin, int amax, int smin, int smax)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.mmin = mmin;
            this.mmax = mmax;
            this.amin = amin;
            this.amax = amax;
            this.smin = smin;
            this.smax = smax;
        }

        public override string ToString()
        {
            return string.Format("x [{0,4}..{1,-4}], m [{2,4}..{3,-4}], a [{4,4}..{5,-4}], s[{6,4}..{7,-4}], accepted: {8}", xmin, xmax,
                mmin, mmax, amin, amax, smin, smax, accepted);
        }

        public (range?, range?) splitRange(Instruction instruction)
        {
            range range1 = (range)MemberwiseClone();
            range range2 = (range)MemberwiseClone();

            switch (instruction.category)
            {
            case 'x':
                if (xmax < instruction.range)
                {
                    return (this, null);
                }

                if (instruction.range < xmin)
                {
                    return (null, this);
                }
                
                if ((bool)instruction.greaterthen!)
                {
                    range1.xmax = (int)instruction.range!;
                    range2.xmin = (int)instruction.range! + 1;
                    return (range1, range2);
                }
                else
                {
                    range1.xmax = (int)instruction.range! - 1;
                    range2.xmin = (int)instruction.range! ;
                    return (range1, range2);
                }
            case 'm':
                if (mmax < instruction.range)
                {
                    return (this, null);
                }

                if (instruction.range < mmin)
                {
                    return (null, this);
                }
                
                if ((bool)instruction.greaterthen!)
                {
                    range1.mmax = (int)instruction.range!;
                    range2.mmin = (int)instruction.range! + 1;
                    return (range1, range2);
                }
                else
                {
                    range1.mmax = (int)instruction.range! - 1;
                    range2.mmin = (int)instruction.range! ;
                    return (range1, range2);
                }
            case 'a':
                if (amax < instruction.range)
                {
                    return (this, null);
                }

                if (instruction.range < amin)
                {
                    return (null, this);
                }
                
                if ((bool)instruction.greaterthen!)
                {
                    range1.amax = (int)instruction.range!;
                    range2.amin = (int)instruction.range! + 1;
                    return (range1, range2);
                }
                else
                {
                    range1.amax = (int)instruction.range! - 1;
                    range2.amin = (int)instruction.range! ;
                    return (range1, range2);
                }
            case 's':
                if (smax < instruction.range)
                {
                    return (this, null);
                }

                if (instruction.range < smin)
                {
                    return (null, this);
                }
                
                if ((bool)instruction.greaterthen!)
                {
                    range1.smax = (int)instruction.range!;
                    range2.smin = (int)instruction.range! + 1;
                    return (range1, range2);
                }
                else
                {
                    range1.smax = (int)instruction.range! - 1;
                    range2.smin = (int)instruction.range! ;
                    return (range1, range2);
                }
            default:
                return (this, null);
            }
        }

        public List<range> executeInstructions(List<Instruction> instructions)
        {
            List<range> ret = new List<range>();
            if (instructions.Count == 1)
            {
                if (instructions[0].result.Length == 1)
                {
                    accepted = instructions[0].result.Equals("A");
                    ret.Add(this);
                    return ret;
                }

                List<Instruction> newInstructions;
                Workflows.TryGetValue(instructions[0].result, out newInstructions!);
                return executeInstructions(newInstructions);
            }
            
            (range? firstrange, range? secondrange) = splitRange(instructions[0]);
            if (secondrange is null)
            {
                if (instructions[0].result.Length == 1)
                {
                    firstrange!.accepted = instructions[0].result.Equals("A");
                    ret.Add(firstrange);
                    return ret;
                }
                List<Instruction> newInstructions;
                Workflows.TryGetValue(instructions[0].result, out newInstructions!);
                return firstrange!.executeInstructions(newInstructions);
            }
            else if (firstrange is null)
            {
                return secondrange!.executeInstructions(instructions.Skip(1).Take(instructions.Count - 1).ToList());
            }
            else
            {
                if ((bool)instructions[0].greaterthen!)
                {
                    range temp = (range)firstrange.MemberwiseClone();
                    firstrange = (range)secondrange.MemberwiseClone();
                    secondrange = (range)temp.MemberwiseClone();
                }
                List<Instruction> newInstructions;
                Workflows.TryGetValue(instructions[0].result, out newInstructions!);
                if (instructions[0].result.Length == 1)
                {
                    firstrange!.accepted = instructions[0].result.Equals("A");
                    ret.Add(firstrange);
                }
                else
                {
                    ret.AddRange(firstrange!.executeInstructions(newInstructions));
                }
                ret.AddRange(secondrange!.executeInstructions(instructions.Skip(1).Take(instructions.Count - 1).ToList()));
                return ret;
            }
        }
    }
    
    static Dictionary<string, List<Instruction>> Workflows = new Dictionary<string, List<Instruction>>();

    private bool nextWorkFlow(Instruction instruction, Part part)
    {
        
        if (instruction.result.Equals("R"))
        {
            return false;
        }

        if (instruction.result.Equals("A"))
        {
            return true;
        }

        List<Instruction> nextworkflow;
        Workflows.TryGetValue(instruction.result, out nextworkflow);
        return runWorkFlow(nextworkflow, part);
    }

    private bool runWorkFlow(List<Instruction> workflow, Part part)
    {
        foreach (Instruction instruction in workflow)
        {
            if (instruction.category is null)
            {
                return nextWorkFlow(instruction, part);
            }

            switch (instruction.category)
            {
                case 'x':
                {
                    if ((bool)instruction.greaterthen!)
                    {
                        if (part.x > instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }
                    else
                    {
                        if (part.x < instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }

                    break;
                }
                case 'm':
                {
                    if ((bool)instruction.greaterthen!)
                    {
                        if (part.m > instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }
                    else
                    {
                        if (part.m < instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }

                    break;
                }
                case 'a':
                {
                    if ((bool)instruction.greaterthen!)
                    {
                        if (part.a > instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }
                    else
                    {
                        if (part.a < instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }

                    break;
                }
                case 's':
                {
                    if ((bool)instruction.greaterthen!)
                    {
                        if (part.s > instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }
                    else
                    {
                        if (part.s < instruction.range)
                        {
                            return nextWorkFlow(instruction, part);
                        }
                    }

                    break;
                }
            }
        }
        Console.WriteLine("Default return used!");
        return false;
    }

    private struct Instruction
    {
        public char? category;
        public bool? greaterthen;
        public int? range;
        public string result;

        public Instruction(string input)
        {
            if (input.Contains(":"))
            {
                int splitindex = input.IndexOf(":");
                category = input[0];
                greaterthen = input[1].Equals('>');
                range = int.Parse(input.Substring(2, splitindex - 2));
                result = input[(splitindex + 1)..];
            }
            else
            {
                result = input;
            }
        }
    }
    
    public override void Run()
    {
        List<Part> parts = new List<Part>();
        bool blanklineread = false;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            if (line.Equals(String.Empty))
            {
                break;
            }

            if (!blanklineread)
            {
                List<Instruction> instructions = new List<Instruction>();
                string[] vals = line[0..^1].Split("{");
                foreach (string val in vals[1].Split(","))
                {
                    instructions.Add(new Instruction(val));
                }
                Workflows.Add(vals[0], instructions);
            }
            else
            {
                string[] vals = line[1..^1].Split(",");
                Part newPart = new Part();
                newPart.x = int.Parse(vals[0].Split("=")[1]);
                newPart.m = int.Parse(vals[1].Split("=")[1]);
                newPart.a = int.Parse(vals[2].Split("=")[1]);
                newPart.s = int.Parse(vals[3].Split("=")[1]);
                parts.Add(newPart);
            }
        }

        range startrange = new range(1, 4000, 1, 4000, 1, 4000, 1, 4000);

        List<Instruction> start;
        Workflows.TryGetValue("in", out start!);
        List<range> ranges = startrange.executeInstructions(start);

        long total = 0;

        foreach (range range in ranges)
        {
            if ((bool)range.accepted!)
            {
                total += (long)(range.xmax - range.xmin + 1) * (range.mmax - range.mmin + 1) * (range.amax - range.amin + 1) * (range.smax - range.smin + 1);
            }
        }

        Console.WriteLine(total);
    }
}