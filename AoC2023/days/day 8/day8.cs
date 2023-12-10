using AoC2023.tools;

namespace AoC2023.days.day_8;

public class Day8: Day
{
    public Day8()
    {
        this.Directory = "day 8";
    }
        
    private static Dictionary<string, string[]> nodes = new Dictionary<string, string[]>();

    private class NodeRoute
    {
        public string currentnode;

        public NodeRoute(string startnode)
        {
            currentnode = startnode;
        }

        private bool OnFinishingSpot()
        {
            return (currentnode[^1].Equals('Z'));
        }

        public bool NextStep(char instruction)
        {
            switch (instruction)
            {
                case 'L':
                {
                    currentnode = nodes.GetValueOrDefault(currentnode)[0];
                    break;
                }
                case 'R':
                {
                    currentnode = nodes.GetValueOrDefault(currentnode)[1];
                    break;
                }
            }

            return OnFinishingSpot();
        }
    }
    
    public override void Run()
    {
        StreamReader data = LoadData();
        string? line;

        string instructions = data.ReadLine()!;
        data.ReadLine(); // clear blank line

        while ((line = data.ReadLine()) != null)
        {
            string[] input = line.Split("=", StringSplitOptions.TrimEntries);
            nodes.Add(input[0], input[1].Substring(1, input[1].Length - 2).Split(",", StringSplitOptions.TrimEntries));
        }

        List<NodeRoute> routes = new List<NodeRoute>();
        foreach (string key in nodes.Keys)
        {
            if (key[^1].Equals('A'))
            {
                routes.Add(new NodeRoute(key));
            }
        }

        List<List<(string nodes, int steps)>> routeEndPoints = new List<List<(string nodes, int steps)>>();
        foreach (NodeRoute route in routes)
        {
            int steps = 1;
            (string node, int steps)? firstEndNode = null;
            List<(string node, int steps)> endNodes = new List<(string, int)>();

            while (true)
            {
                bool endNode = route.NextStep(instructions[(steps - 1) % instructions.Length]);
                if (endNode)
                {
                    endNodes.Add((route.currentnode, steps));
                    if (firstEndNode is null)
                    {
                        firstEndNode = (route.currentnode, steps);
                    }
                    else
                    {
                        if (route.currentnode.Equals(firstEndNode.Value.node) && steps % instructions.Length ==
                            firstEndNode.Value.steps % instructions.Length)
                        {
                            break;
                        }
                    }
                }

                steps++;
            }
            routeEndPoints.Add(endNodes);
        }

        List<int> cycleLengths = new List<int>();
        foreach (List<(string node, int steps)> endpoints in routeEndPoints)
        {
            cycleLengths.Add(endpoints[^1].steps - endpoints[0].steps);
        }
        
        Console.WriteLine(Utils.LCM(cycleLengths.ToArray()));
    }
}

