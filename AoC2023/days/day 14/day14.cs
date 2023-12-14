using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_14;

public class Day14: Day
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
        Round,
        Cube
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

        public void TiltGrid(Cardinal cardinal)
        {
            switch (cardinal)
            {
                case Cardinal.North:
                    for (int y = 0; y < grid.Count; y++)
                    {
                        for (int x = 0; x < grid[0].Length; x++)
                        {
                            grid[y][x].Roll(cardinal);
                        }
                    }
                    break;
                case Cardinal.East:
                    for (int x = grid[0].Length - 1; x >= 0; x--)
                    {
                        for (int y = 0; y < grid.Count; y++)
                        {
                            grid[y][x].Roll(cardinal);
                        }
                    }
                    break;
                case Cardinal.South:
                    for (int y = grid.Count - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < grid[0].Length; x++)
                        {
                            grid[y][x].Roll(cardinal);
                        }
                    }
                    break;
                case Cardinal.West:
                    for (int x = 0; x < grid[0].Length; x++)
                    {
                        for (int y = 0; y < grid.Count; y++)
                        {
                            grid[y][x].Roll(cardinal);
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardinal), cardinal, null);
            }
        }

        public void SwapGridSpaces(GridSpace space1, GridSpace space2)
        {
            swapcount++;
            
            int space1x = space1.Coordinate.x;
            int space1y = space1.Coordinate.y;
            int space2x = space2.Coordinate.x;
            int space2y = space2.Coordinate.y;

            grid[space1y][space1x] = space2;
            grid[space2y][space2x] = space1;

            space1.Coordinate = new Coordinate(space2x, space2y);
            space2.Coordinate = new Coordinate(space1x, space1y);
        }

        public new string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (GridSpace[] gridline in grid)
            {
                foreach (GridSpace space in gridline)
                {
                    switch (space.Type)
                    {
                        case SpaceType.Round:
                        {
                            builder.Append("O");
                            break;
                        }
                        case SpaceType.Empty:
                        {
                            builder.Append(".");
                            break;
                        }
                        case SpaceType.Cube:
                        {
                            builder.Append("#");
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return builder.ToString();
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
                case '#':
                {
                    Type = SpaceType.Cube;
                    break;
                }
                case 'O':
                {
                    Type = SpaceType.Round;
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

        public void Roll(Cardinal cardinal)
        {
            if (!Type.Equals(SpaceType.Round))
            {
                return;
            }
            
            bool adjacentIsFree = true;
            while (adjacentIsFree)
            {
                Coordinate adjacentCoordinate = GetAdjacentCoordinate(cardinal);
                GridSpace? adjacent = Parent.getSpace(adjacentCoordinate);
                if (adjacent is null)
                {
                    adjacentIsFree = false;
                    continue;
                }

                adjacentIsFree = adjacent.Type.Equals(SpaceType.Empty);
                if (adjacentIsFree)
                {
                    Parent.SwapGridSpaces(this, adjacent);
                }
            }
        }
    }
    public Day14()
    {
        this.Directory = "day 14";
    }
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

        Dictionary<string, long> memo = new Dictionary<string, long>();
        long breakiterat = -1;
        long spincycles = 1000000000;
        long cyclestart = 0;

        for (long iter = 1; iter < spincycles && iter != breakiterat; iter++)
        {
            grid.swapcount = 0;
            grid.TiltGrid(Cardinal.North);
            grid.TiltGrid(Cardinal.West);
            grid.TiltGrid(Cardinal.South);
            grid.TiltGrid(Cardinal.East);
            if (grid.swapcount == 0)
            {
                break;
            }

            if (breakiterat < 0)
            {
                string gridAsString = grid.ToString();

                if (memo.TryGetValue(gridAsString, out cyclestart))
                {
                    long cyclelength = iter - cyclestart;
                    long breakiteratposition = ((spincycles - cyclestart) % cyclelength);
                    breakiterat = cyclestart + cyclelength + breakiteratposition;
                }
                else
                {
                    memo.Add(gridAsString, iter);
                }
            }
        }

        long totalweight = 0;

        for (int y = 0; y < grid.grid.Count; y++)
        {
            for (int x = 0; x < grid.grid[0].Length; x++)
            {
                GridSpace? space = grid.getSpace(new Coordinate(x, y));
                if (space is not null && space.Type.Equals(SpaceType.Round))
                {
                    totalweight += grid.grid.Count - space.Coordinate.y;
                }
            }
        }
        
        Console.WriteLine(totalweight);
    }
}