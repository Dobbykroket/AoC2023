using AoC2023.tools;

namespace AoC2023.days.day_10;

public class Day10: Day
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
        Null,
        Pipe,
        Rightside,
        Leftside
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

    private Cardinal ClockwiseCardinal(Cardinal cardinal)
    {
        switch (cardinal)
        {
            case Cardinal.North:
            {
                return Cardinal.East;
            }
            case Cardinal.South:
            {
                return Cardinal.West;
            }
            case Cardinal.East:
            {
                return Cardinal.South;
            }
            case Cardinal.West:
            default:
            {
                return Cardinal.North;
            }
        }
    }

    private Cardinal CounterClockwiseCardinal(Cardinal cardinal)
    {
        switch (cardinal)
        {
            case Cardinal.North:
            {
                return Cardinal.West;
            }
            case Cardinal.South:
            {
                return Cardinal.East;
            }
            case Cardinal.East:
            {
                return Cardinal.North;
            }
            case Cardinal.West:
            default:
            {
                return Cardinal.South;
            }
        }
    }
    
    private struct Coordinate
    {
        public readonly int x;
        public readonly int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private class GridSpace
    {
        public Coordinate Coordinate;
        public List<Cardinal> Cardinals = new();
        public readonly bool StartPosition = false;
        public SpaceType Type;
        private readonly Grid Parent;

        public GridSpace(Coordinate coordinate, char content, Grid parent)
        {
            Coordinate = coordinate;
            Parent = parent;

            switch (content)
            {
                case '|':
                {
                    Cardinals.Add(Cardinal.North);
                    Cardinals.Add(Cardinal.South);
                    break;
                }
                case '-':
                {
                    Cardinals.Add(Cardinal.East);
                    Cardinals.Add(Cardinal.West);
                    break;
                }
                case 'L':
                {
                    Cardinals.Add(Cardinal.North);
                    Cardinals.Add(Cardinal.East);
                    break;
                }
                case 'J':
                {
                    Cardinals.Add(Cardinal.North);
                    Cardinals.Add(Cardinal.West);
                    break;
                }
                case '7':
                {
                    Cardinals.Add(Cardinal.West);
                    Cardinals.Add(Cardinal.South);
                    break;
                }
                case 'F':
                {
                    Cardinals.Add(Cardinal.East);
                    Cardinals.Add(Cardinal.South);
                    break;
                }
                case '.':
                {
                    break;
                }
                case 'S':
                {
                    StartPosition = true;
                    break;
                }
            }
        }

        public Cardinal GetOtherExit(Cardinal cardinal)
        {
            return Cardinals.First(card => !card.Equals(cardinal));
        }

        public Coordinate GetAdjacentCoordinate(Cardinal cardinal)
        {
            switch (cardinal) // This has the chance to return out of bounds coordinates, but that should not happen in this problem
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

        public (Coordinate, Cardinal) GetNextCoordinate(Cardinal cardinal)
        {
            Cardinal direction = GetOtherExit(cardinal);
            Coordinate next = GetAdjacentCoordinate(direction);
            return (next, direction);
        }

        public void SpreadType()
        {
            foreach (Cardinal cardinal in Enum.GetValues(typeof(Cardinal)))
            {
                GridSpace? adjacentGridSpace = Parent.getSpace(GetAdjacentCoordinate(cardinal));
                if (adjacentGridSpace is null || adjacentGridSpace.Type.Equals(SpaceType.Pipe) || adjacentGridSpace.Type.Equals(Type))
                {
                    continue;
                }

                adjacentGridSpace.Type = Type;
                adjacentGridSpace.SpreadType();
            }
        }
    }

    private class Grid
    {
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

    private GridSpace NextGridSpace(Grid grid, Coordinate nextCoordinate, Cardinal nextDirection)
    {
        var nextGridSpace = grid.getSpace(nextCoordinate)!;
        nextGridSpace.Type = SpaceType.Pipe;
        if (nextGridSpace.StartPosition)
        {
            return nextGridSpace;
        }
        if (nextGridSpace.GetOtherExit(flipCardinal(nextDirection)).Equals(ClockwiseCardinal(nextDirection)))
        {
            GridSpace? frontGridSpace = grid.getSpace(nextGridSpace.GetAdjacentCoordinate(nextDirection));
            if (frontGridSpace is not null && frontGridSpace.Type != SpaceType.Pipe)
            {
                frontGridSpace.Type = SpaceType.Leftside;
            }
        }
        else
        {
            GridSpace? rightGridSpace =
                grid.getSpace(nextGridSpace.GetAdjacentCoordinate(ClockwiseCardinal(nextDirection)));
            if (rightGridSpace is not null && rightGridSpace.Type != SpaceType.Pipe)
            {
                rightGridSpace.Type = SpaceType.Rightside;
            }
        }
        
        if (nextGridSpace.GetOtherExit(flipCardinal(nextDirection)).Equals(CounterClockwiseCardinal(nextDirection)))
        {
            GridSpace? frontGridSpace = grid.getSpace(nextGridSpace.GetAdjacentCoordinate(nextDirection));
            if (frontGridSpace is not null && frontGridSpace.Type != SpaceType.Pipe)
            {
                frontGridSpace.Type = SpaceType.Rightside;
            }
        }
        else
        {
            GridSpace? leftGridSpace =
                grid.getSpace(nextGridSpace.GetAdjacentCoordinate(CounterClockwiseCardinal(nextDirection)));
            if (leftGridSpace is not null && leftGridSpace.Type != SpaceType.Pipe)
            {
                leftGridSpace.Type = SpaceType.Leftside;
            }
        }

        return nextGridSpace;
    }
    
    public Day10()
    {
        this.Directory = "day 10";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        Grid grid = new Grid();
        Coordinate startposition = new Coordinate(-1, -1);
        int y = 0;
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            List<GridSpace> gridLine = new List<GridSpace>();
            for (int x = 0; x < line.Length; x++)
            {
                gridLine.Add(new GridSpace(new Coordinate(x, y), line[x], grid));
                if (line[x].Equals('S'))
                {
                    startposition = new Coordinate(x, y);
                }
            }
            grid.grid.Add(gridLine.ToArray());
            y++;
        }

        //Add a blank line to the bottom to ensure we always have some free space between the pipe and the outside
        List<GridSpace> LastGridLine = new List<GridSpace>();
        for (int x = 0; x < grid.grid[^1].Length; x++)
        {
            LastGridLine.Add(new GridSpace(new Coordinate(x, y), '.', grid));
        }
        grid.grid.Add(LastGridLine.ToArray());

        List<Coordinate> route = new List<Coordinate>();
        GridSpace start = grid.getSpace(startposition)!;

        GridSpace nextGridSpace;
        Coordinate nextCoordinate;
        Cardinal nextDirection;
        
        if (grid.getSpace(start.GetAdjacentCoordinate(Cardinal.North)) is not null 
            && grid.getSpace(start.GetAdjacentCoordinate(Cardinal.North))!.Cardinals.Contains(Cardinal.South))
        {
            nextCoordinate = start.GetAdjacentCoordinate(Cardinal.North);
            nextDirection = Cardinal.North;
        }
        else if (grid.getSpace(start.GetAdjacentCoordinate(Cardinal.East)) != null 
                 && grid.getSpace(start.GetAdjacentCoordinate(Cardinal.East))!.Cardinals.Contains(Cardinal.West))
        {
            nextCoordinate = start.GetAdjacentCoordinate(Cardinal.East);
            nextDirection = Cardinal.East;
        }
        else
        {
            nextCoordinate = start.GetAdjacentCoordinate(Cardinal.South); // if not North || East then South && West
            nextDirection = Cardinal.South;
        }
        route.Add(nextCoordinate);
        nextGridSpace = NextGridSpace(grid, nextCoordinate, nextDirection);

        while (!nextGridSpace.StartPosition)
        {
            (nextCoordinate, nextDirection) = nextGridSpace.GetNextCoordinate(flipCardinal(nextDirection));
            route.Add(nextCoordinate);
            nextGridSpace = NextGridSpace(grid, nextCoordinate, nextDirection);
        }

        foreach (GridSpace[] gridline in grid.grid)
        {
            foreach (GridSpace space in gridline)
            {
                if (!(space.Type.Equals(SpaceType.Leftside) || space.Type.Equals(SpaceType.Rightside)))
                {
                    continue;
                }
                space.SpreadType();
            }
        }

        SpaceType outside = SpaceType.Null;

        foreach (GridSpace space in grid.grid[0])
        {
            if (!space.Type.Equals(SpaceType.Pipe))
            {
                outside = space.Type;
                break;
            }
        }
        
        if (outside.Equals(SpaceType.Null))
        {
            foreach(GridSpace[] gridline in grid.grid)
            {
                if (!gridline[0].Type.Equals(SpaceType.Pipe))
                {
                    outside = gridline[0].Type;
                    break;
                }
                if (!gridline[^1].Type.Equals(SpaceType.Pipe))
                {
                    outside = gridline[^1].Type;
                    break;
                }
            }
        }

        if (outside.Equals(SpaceType.Null))
        {
            foreach (GridSpace space in grid.grid[^1])
            {
                if (!space.Type.Equals(SpaceType.Pipe))
                {
                    outside = space.Type;
                    break;
                }
            }
        }

        Console.WriteLine("Distance of furthest pipe: {0}", route.Count / 2);
        if (!outside.Equals(SpaceType.Rightside))
        {
            Console.WriteLine("Count of rightside is: {0}", grid.grid.Sum(gridline => gridline.Count(space => space.Type.Equals(SpaceType.Rightside))));
        }
        if (!outside.Equals(SpaceType.Leftside))
        {
            Console.WriteLine("Count of leftside is: {0}", grid.grid.Sum(gridline => gridline.Count(space => space.Type.Equals(SpaceType.Leftside))));
        }

        foreach (GridSpace[] gridline in grid.grid)
        {
            foreach (GridSpace space in gridline)
            {
                if (space.Type.Equals(SpaceType.Null))
                {
                    Console.WriteLine("{0}, {1}", space.Coordinate.x, space.Coordinate.y);
                }
            }
        }
    }
}