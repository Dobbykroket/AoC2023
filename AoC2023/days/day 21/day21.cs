using AoC2023.tools;

namespace AoC2023.days.day_21;

public class Day21: Day
{
    private enum Cardinal
    {
        North,
        East,
        South,
        West
    }

    private enum SpaceType
    {
        Empty,
        Garden,
        Rock
    }

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

    private Cardinal flipCardinal(Cardinal cardinal)
    {
        switch (cardinal)
        {
            case Cardinal.North:
            {
                return Cardinal.South;
            }
            case Cardinal.South:
            {
                return Cardinal.North;
            }
            case Cardinal.East:
            {
                return Cardinal.West;
            }
            case Cardinal.West:
            default:
            {
                return Cardinal.East;
            }
        }
    }
    
    private class Grid
    {
        public int swapcount = 0;
        public List<GridSpace[]> grid = new List<GridSpace[]>();

        public GridSpace? getSpace(Coordinate coordinate)
        {
            try
            {
                return grid[coordinate.y][coordinate.x];
            }
            catch (Exception ex) when (
                ex is IndexOutOfRangeException
                    or NullReferenceException
                    or ArgumentOutOfRangeException
            )
            {
                return null;
            }
        }
    }

    private class GridSpace
    {
        private readonly Grid Parent;
        public SpaceType Type;
        public Coordinate Coordinate;

        public GridSpace(Coordinate coordinate, char c, Grid grid)
        {
            Parent = grid;
            Coordinate = coordinate;
            switch (c)
            {
                case '.':
                {
                    Type = SpaceType.Garden;
                    break;
                }
                case '#':
                {
                    Type = SpaceType.Rock;
                    break;
                }
                default:
                {
                    Type = SpaceType.Empty;
                    break;
                }
            }
        }

        public Coordinate GetAdjacentCoordinate(Cardinal cardinal)
        {
            switch
                (cardinal) // This has the chance to return out of bounds coordinates, but that should not happen in this problem
            {
                case Cardinal.North:
                {
                    return new Coordinate(Coordinate.x, Coordinate.y - 1);
                }
                case Cardinal.East:
                {
                    return new Coordinate(Coordinate.x + 1, Coordinate.y);
                }
                case Cardinal.South:
                {
                    return new Coordinate(Coordinate.x, Coordinate.y + 1);
                }
                case Cardinal.West:
                default:
                {
                    return new Coordinate(Coordinate.x - 1, Coordinate.y);
                }
            }
        }

        public GridSpace? GetAdjacentSpace(Cardinal cardinal)
        {
            return Parent.getSpace(GetAdjacentCoordinate(cardinal));
        }

        public List<GridSpace> GetAllAdjacentSpaces()
        {
            List<GridSpace?> ret = new List<GridSpace?>();
            ret.Add(GetAdjacentSpace(Cardinal.North));
            ret.Add(GetAdjacentSpace(Cardinal.East));
            ret.Add(GetAdjacentSpace(Cardinal.South));
            ret.Add(GetAdjacentSpace(Cardinal.West));

            ret.RemoveAll(gs => gs is null);

            return (List<GridSpace>)ret!;
        }
    }
    public Day21()
    {
        this.Directory = "day 21";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        int stepsToTake = 64;
        string? line;
        Grid grid = new Grid();
        Coordinate startCoords = new Coordinate();
        int initY = 0;
        while ((line = data.ReadLine()) != null)
        {
            List<GridSpace> gridLine = new List<GridSpace>();
            for (int x = 0; x < line.Length; x++)
            {
                gridLine.Add(new GridSpace(new Coordinate(x, initY), line[x], grid));
                if (line[x].Equals('S'))
                {
                    startCoords = new Coordinate(x, initY);
                }
            }

            grid.grid.Add(gridLine.ToArray());
            initY++;
        }

        GridSpace start = grid.getSpace(startCoords)!;

        List<GridSpace> seen = new List<GridSpace>();
        List<GridSpace> answer = new List<GridSpace>();
        List<GridSpace> currentBoundary = new List<GridSpace>();
        currentBoundary.Add(start);
        
        

        if (stepsToTake % 2 == 0)
        {
            answer.AddRange(currentBoundary);
        }

        for (int i = 1; i <= stepsToTake; i++)
        {
            List<GridSpace> currentOptions = new List<GridSpace>();
            foreach (GridSpace space in currentBoundary)
            {
                currentOptions.AddRange(space.GetAllAdjacentSpaces());
            }
            currentBoundary.Clear();
            foreach (GridSpace space in currentOptions)
            {
                if (space.Type == SpaceType.Garden)
                {
                    if (!seen.Contains(space))
                    {
                        seen.Add(space);
                        currentBoundary.Add(space);
                    }
                }
            }

            if (i % 2 == stepsToTake % 2)
            {
                answer.AddRange(currentBoundary);
            }
        }
        
        Console.WriteLine(answer.Count);
    }
}