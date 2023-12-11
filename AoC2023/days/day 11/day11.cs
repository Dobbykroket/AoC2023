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
        public bool horizontalEmpty = false;
        public bool verticalEmpty = false;

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
        int expansionfactor = 1000000;
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
                    gridline[i].horizontalEmpty = true;
                }
            }

        }

        for (int i = grid.Count - 1; i >= 0; i--)
        {
            if (grid[i].All(gridspace => !gridspace.hasStar))
            {
                foreach (GridSpace space in grid[i])
                {
                    space.verticalEmpty = true;
                }
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
        int emptycrossings = 0;

        for (int i = 0; i < stars.Count; i++)
        {
            for (int j = i + 1; j < stars.Count; j++)
            {
                int hordistance = 0;
                int vertdistance = 0;
                int startX, endX;
                int startY, endY;
                if (stars[i].x >= stars[j].x)
                {
                    endX = stars[i].x;
                    startX = stars[j].x;
                }
                else
                {
                    endX = stars[j].x;
                    startX = stars[i].x;
                }
                if (stars[i].y >= stars[j].y)
                {
                    endY = stars[i].y;
                    startY = stars[j].y;
                }
                else
                {
                    endY = stars[j].y;
                    startY = stars[i].y;
                }

                hordistance = endX - startX;
                vertdistance = endY - startY;
                totaldistance += hordistance + vertdistance;

                for (int x = startX + 1; x <= endX; x++)
                {
                    if (grid[startY][x].horizontalEmpty)
                    {
                        emptycrossings++;
                    }
                }

                for (int y = startY + 1; y <= endY; y++)
                {
                    if (grid[y][endX].verticalEmpty)
                    {
                        emptycrossings++;
                    }
                }
            }
        }
        
        Console.WriteLine(emptycrossings);
        Console.WriteLine(totaldistance);
        Console.WriteLine((long) totaldistance + ((long) emptycrossings) * (expansionfactor - 1));
    }
}