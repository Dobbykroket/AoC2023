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

        public GridSpace(Coordinate coordinate, char content)
        {
            Coordinate = coordinate;

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
    }

    private class Grid
    {
        public GridSpace[][] grid;

        public Grid(long lenght)
        {
            grid = new GridSpace[lenght][];
        }

        public GridSpace getSpace(Coordinate coordinate)
        {
            return grid[coordinate.y][coordinate.x];
        }
    }
    
    public Day10()
    {
        this.Directory = "day 10";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        Grid grid = new Grid(data.BaseStream.Length);
        Coordinate startposition = new Coordinate(-1, -1);
        int y = 0;
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            List<GridSpace> gridLine = new List<GridSpace>();
            for (int x = 0; x < line.Length; x++)
            {
                gridLine.Add(new GridSpace(new Coordinate(x, y), line[x]));
                if (line[x].Equals('S'))
                {
                    startposition = new Coordinate(x, y);
                }
            }
            grid.grid[y] = gridLine.ToArray();
            y++;
        }

        List<Coordinate> route = new List<Coordinate>();
        GridSpace start = grid.getSpace(startposition);

        GridSpace nextGridSpace;
        Coordinate nextCoordinate;
        Cardinal nextDirection;
        
        if (grid.getSpace(start.GetAdjacentCoordinate(Cardinal.North)).Cardinals.Contains(Cardinal.South))
        {
            nextCoordinate = start.GetAdjacentCoordinate(Cardinal.North);
            nextDirection = Cardinal.North;
        }
        else if (grid.getSpace(start.GetAdjacentCoordinate(Cardinal.East)).Cardinals.Contains(Cardinal.West))
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
        nextGridSpace = grid.getSpace(nextCoordinate);

        while (!nextGridSpace.StartPosition)
        {
            (nextCoordinate, nextDirection) = nextGridSpace.GetNextCoordinate(flipCardinal(nextDirection));
            route.Add(nextCoordinate);
            nextGridSpace = grid.getSpace(nextCoordinate);
        }

        Console.WriteLine(route.Count / 2);
    }
}