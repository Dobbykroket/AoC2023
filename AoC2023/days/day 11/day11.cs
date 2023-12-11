using AoC2023.tools;

namespace AoC2023.days.day_11;

public class Day11: Day
{
    
    private struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private class GridSpace
    {
        public bool hasStar = false;

        public GridSpace(char c)
        {
            hasStar = c.Equals('#');
        }
    }
    
    public Day11()
    {
        this.Directory = "day 11";
    }
    public override void Run()
    {
        List<List<GridSpace>> grid = new List<List<GridSpace>>();
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            List<GridSpace> gridline = new List<GridSpace>();
            foreach (char c in line)
            {
                gridline.Add(new GridSpace(c));
            }
            grid.Add(gridline);
        }

        for (int i = grid[0].Count - 1; i >= 0; i--)
        {
            if (
                grid.Select(gridline => gridline[i])
                .All(gridspace => !gridspace.hasStar))
            {
                foreach (List<GridSpace> gridline in grid)
                {
                    gridline.Insert(i, new GridSpace('.'));
                }
            }

        }

        for (int i = grid.Count - 1; i >= 0; i--)
        {
            if (grid[i].All(gridspace => !gridspace.hasStar))
            {
                grid.Insert(i, grid[i].ToList() );
            }
        }

        List<Coordinate> stars = new List<Coordinate>();

        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                if (grid[y][x].hasStar)
                {
                    stars.Add(new Coordinate(x, y));
                }
            }
        }

        int totaldistance = 0;

        for (int i = 0; i < stars.Count; i++)
        {
            for (int j = i + 1; j < stars.Count; j++)
            {
                int hordistance = Math.Abs(stars[i].x - stars[j].x);
                int vertdistance = Math.Abs(stars[i].y - stars[j].y);
                totaldistance += hordistance + vertdistance;
            }
        }
        
        Console.WriteLine(totaldistance);
    }
}