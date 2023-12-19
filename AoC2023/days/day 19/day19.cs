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
    
    static Dictionary<string, string[]> Workflows = new Dictionary<string, string[]>();

    private bool nextWorkFlow(string instruction, Part part)
    {
        
        if (instruction.Equals("R"))
        {
            return false;
        }

        if (instruction.Equals("A"))
        {
            return true;
        }

        string[] nextworkflow;
        Workflows.TryGetValue(instruction, out nextworkflow);
        return runWorkFlow(nextworkflow, part);
    }

    private bool runWorkFlow(string[] workflow, Part part)
    {
        foreach (string step in workflow)
        {
            if (!step.Contains(':'))
            {
                return nextWorkFlow(step, part);
            }

            string[] instructions = step.Split(":");
            switch (instructions[0][0])
            {
                case 'x':
                {
                    if (instructions[0][1].Equals('<'))
                    {
                        if (part.x < int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }
                    else
                    {
                        if (part.x > int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }

                    break;
                }
                case 'm':
                {
                    if (instructions[0][1].Equals('<'))
                    {
                        if (part.m < int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }
                    else
                    {
                        if (part.m > int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }

                    break;
                }
                case 'a':
                {
                    if (instructions[0][1].Equals('<'))
                    {
                        if (part.a < int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }
                    else
                    {
                        if (part.a > int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }

                    break;
                }
                case 's':
                {
                    if (instructions[0][1].Equals('<'))
                    {
                        if (part.s < int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }
                    else
                    {
                        if (part.s > int.Parse(instructions[0][2..]))
                        {
                            return nextWorkFlow(instructions[1], part);
                        }
                    }

                    break;
                }
            }
        }
        Console.WriteLine("Default return used!");
        return false;
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
                blanklineread = true;
                continue;
            }

            if (!blanklineread)
            {
                string[] vals = line[0..^1].Split("{");
                string[] instructions = vals[1].Split(",");
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

        long total = 0;

        foreach (Part part in parts)
        {
            if (nextWorkFlow("in", part))
            {
                total += part.x + part.m + part.a + part.s;
            }
        }
        
        Console.WriteLine(total);
    }
}