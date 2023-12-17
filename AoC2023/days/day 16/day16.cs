using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_16;

public class Day16 : Day
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
        SplitterVertical,
        SplitterHorizontal,
        MirrorBottomRight,
        MirrorBottomLeft
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
        public bool energized = false;

        public GridSpace(Coordinate coordinate, char c, Grid grid)
        {
            Parent = grid;
            Coordinate = coordinate;
            switch (c)
            {
                case '|':
                {
                    Type = SpaceType.SplitterVertical;
                    break;
                }
                case '-':
                {
                    Type = SpaceType.SplitterHorizontal;
                    break;
                }
                case '\\':
                {
                    Type = SpaceType.MirrorBottomRight;
                    break;
                }
                case '/':
                {
                    Type = SpaceType.MirrorBottomLeft;
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
    }

    public Day16()
    {
        this.Directory = "day 16";
    }
    private void RunLight(Coordinate coordinate, Cardinal cardinal, Grid grid)
    {
        GridSpace? space = grid.getSpace(coordinate);
        if (space is null)
        {
            return;
        }
        if (alreadyRan.Contains((coordinate, cardinal)) || (alreadyRan.Contains((coordinate, flipCardinal(cardinal)))) && !(space.Type.Equals(SpaceType.MirrorBottomLeft) || space.Type.Equals(SpaceType.MirrorBottomRight)))
        {
            return;
        }
        alreadyRan.Add((coordinate, cardinal));

        space.energized = true;

        switch (space.Type)
        {
            case SpaceType.Empty:
                RunLightQueue.Enqueue((space.GetAdjacentCoordinate(cardinal), cardinal));
                break;
            case SpaceType.SplitterHorizontal:
                switch (cardinal)
                {
                    case Cardinal.North:
                    case Cardinal.South:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.East), Cardinal.East));
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.West), Cardinal.West));
                        break;
                    case Cardinal.East:
                    case Cardinal.West:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(cardinal), cardinal));
                        break;
                }

                break;
            case SpaceType.SplitterVertical:
                switch (cardinal)
                {
                    case Cardinal.East:
                    case Cardinal.West:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.North), Cardinal.North));
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.South), Cardinal.South));
                        break;
                    case Cardinal.North:
                    case Cardinal.South:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(cardinal), cardinal));
                        break;
                }

                break;
            case SpaceType.MirrorBottomRight:
                switch (cardinal)
                {
                    case Cardinal.North:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.West), Cardinal.West));
                        break;
                    case Cardinal.East:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.South), Cardinal.South));
                        break;
                    case Cardinal.South:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.East), Cardinal.East));
                        break;
                    case Cardinal.West:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.North), Cardinal.North));
                        break;
                }

                break;
            case SpaceType.MirrorBottomLeft:
                switch (cardinal)
                {
                    case Cardinal.North:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.East), Cardinal.East));
                        break;
                    case Cardinal.East:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.North), Cardinal.North));
                        break;
                    case Cardinal.South:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.West), Cardinal.West));
                        break;
                    case Cardinal.West:
                        RunLightQueue.Enqueue((space.GetAdjacentCoordinate(Cardinal.South), Cardinal.South));
                        break;
                }

                break;
            
        }
    }
        
    private static List<(Coordinate, Cardinal)> alreadyRan = new List<(Coordinate, Cardinal)>();
    private static Queue<(Coordinate, Cardinal)> RunLightQueue = new Queue<(Coordinate, Cardinal)>();

    public override void Run()
    {
        Grid grid = new Grid();
        int initY = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            List<GridSpace> gridLine = new List<GridSpace>();
            for (int x = 0; x < line.Length; x++)
            {
                gridLine.Add(new GridSpace(new Coordinate(x, initY), line[x], grid));
            }

            grid.grid.Add(gridLine.ToArray());
            initY++;
        }

        Queue<(Coordinate, Cardinal)> stillToRun = new Queue<(Coordinate, Cardinal)>();

        for (int x = 0; x < grid.grid[0].Length; x++)
        {
            stillToRun.Enqueue((new Coordinate(x, 0), Cardinal.South));
            stillToRun.Enqueue((new Coordinate(x, grid.grid.Count - 1), Cardinal.North));
        }

        for (int y = 0; y < grid.grid.Count; y++)
        {
            stillToRun.Enqueue((new Coordinate(0, y), Cardinal.East));
            stillToRun.Enqueue((new Coordinate(grid.grid[y].Length - 1, y), Cardinal.West));
        }

        long highestsum = -1;

        while (stillToRun.Count > 0)
        {
            Console.WriteLine("{0} still to go", stillToRun.Count);
            RunLightQueue.Enqueue(stillToRun.Dequeue());

            while (RunLightQueue.Count > 0)
            {
                (Coordinate coordinate, Cardinal cardinal) = RunLightQueue.Dequeue();
                RunLight(coordinate, cardinal, grid);
            }
            
            alreadyRan.Clear();
            
            long sum = 0;

            for (int y = 0; y < grid.grid.Count; y++)
            {
                for (int x = 0; x < grid.grid[0].Length; x++)
                {
                    GridSpace? space = grid.getSpace(new Coordinate(x, y));
                    if (space is not null && space.energized)
                    {
                        if (space.energized)
                        {
                            sum++;
                            space.energized = false;
                        }
                    }
                }
            }

            if (sum > highestsum)
            {
                highestsum = sum;
            }
        }
        
        Console.WriteLine(highestsum);
    }
}