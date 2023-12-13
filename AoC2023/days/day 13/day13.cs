using AoC2023.tools;

namespace AoC2023.days.day_13;

public class Day13: Day
{
    public Day13()
    {
        this.Directory = "day 13";
    }

    private bool VerticalMirror(List<List<char>> grid, int possiblemirror)
    {
        for (int iter = 0; possiblemirror - iter >= 0; iter++)
        {
            if (possiblemirror + 1 + iter >= grid[0].Count)
            {
                return true;
            }

            if (!grid.Select(l => l[possiblemirror - iter]).ToList().SequenceEqual(grid.Select(l => l[possiblemirror + 1 + iter]).ToList()))
            {
                return false;
            }
        }

        return true;
    }

    private bool HorizontalMirror(List<List<char>> grid, int possiblemirror)
    {
        for (int iter = 0; possiblemirror - iter >= 0; iter++)
        {
            if (possiblemirror + 1 + iter >= grid.Count)
            {
                return true;
            }
            if (!grid[possiblemirror - iter].SequenceEqual(grid[possiblemirror + 1 + iter]))
            {
                return false;
            }
        }

        return true;
    }

    private int GridIter(List<List<char>> grid)
    {
        int originalvalue = 0;
                
        // find horizontal mirror
        for (int it = 0; it < grid.Count - 1; it++)
        {
            if (grid[it].SequenceEqual(grid[it + 1]))
            {
                if (HorizontalMirror(grid, it))
                {
                    originalvalue = (100 * (it + 1));
                }
            }
        }
            
        // find vertical mirror
        for (int it = 0; it < grid[0].Count - 1; it++)
        {
            if (grid.Select(l => l[it]).ToList().SequenceEqual(grid.Select(l => l[it + 1]).ToList()))
            {
                if (VerticalMirror(grid, it))
                {
                    originalvalue = it + 1;
                }
            }
        }
        
        for(int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                char original = grid[i][j];
                grid[i][j] = FlipChar(original);
                
                // find horizontal mirror
                for (int it = 0; it < grid.Count - 1; it++)
                {
                    if (grid[it].SequenceEqual(grid[it + 1]))
                    {
                        if (HorizontalMirror(grid, it))
                        {
                            if (100 * (it + 1) != originalvalue)
                            {
                                return (100 * (it + 1));
                            }
                        }
                    }
                }
            
                // find vertical mirror
                for (int it = 0; it < grid[0].Count - 1; it++)
                {
                    if (grid.Select(l => l[it]).ToList().SequenceEqual(grid.Select(l => l[it + 1]).ToList()))
                    {
                        if (VerticalMirror(grid, it))
                        {
                            if ((it + 1) != originalvalue)
                            {
                                return it + 1;
                            }
                        }
                    }
                }

                grid[i][j] = original;
            }
        }

        return 0;
    }
    
    private char FlipChar(char c)
    {
        switch (c)
        {
            case '.':
            {
                return '#';
            }
            case '#':
            {
                return '.';
            }
            default:
            {
                return c;
            }
        }
    }
    
    public override void Run()
    {
        int result = 0;
        List<List<List<char>>> gridList = new List<List<List<char>>>();
        gridList.Add(new List<List<char>>());
        int gridindex = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            if (line.Equals(string.Empty))
            {
                gridindex++;
                gridList.Add(new List<List<char>>());
                continue;
            }
            gridList[gridindex].Add(line.ToList());
        }

        foreach (List<List<char>> grid in gridList)
        {
            result += GridIter(grid);
        }

        Console.WriteLine(result);
    }
}