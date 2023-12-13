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
            // find horizontal mirror
            for (int i = 0; i < grid.Count - 1; i++)
            {
                if (grid[i].SequenceEqual(grid[i + 1]))
                {
                    if (HorizontalMirror(grid, i))
                    {
                        result += (100 * (i + 1));
                    }
                }
            }
            
            // find vertical mirror
            for (int i = 0; i < grid[0].Count - 1; i++)
            {
                if (grid.Select(l => l[i]).ToList().SequenceEqual(grid.Select(l => l[i + 1]).ToList()))
                {
                    if (VerticalMirror(grid, i))
                    {
                        result += i + 1;
                    }
                }
            }
        }

        Console.WriteLine(result);
    }
}