using AoC2023.tools;

namespace AoC2023.days.day_18;

public class Day18: Day
{
    
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
    public Day18()
    {
        this.Directory = "day 18";
    }
    public override void Run()
    {
        List<Coordinate> PolygonPoints = new List<Coordinate>();
        Coordinate CurrentCoordinate = new Coordinate(0, 0);
        PolygonPoints.Add(CurrentCoordinate);
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            string[] split = line.Split(" ");
            switch (split[0])
            {
                case "U":
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x, CurrentCoordinate.y + int.Parse(split[1]));
                    break;
                case "D":
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x, CurrentCoordinate.y - int.Parse(split[1]));
                    break;
                case "L":
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x - int.Parse(split[1]), CurrentCoordinate.y);
                    break;
                case "R":
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x + int.Parse(split[1]), CurrentCoordinate.y);
                    break;
            }
            PolygonPoints.Add(CurrentCoordinate);
        }

        long area = 0;
        long circumference = 0;
        for (int i = 0; i < PolygonPoints.Count - 1; i++)
        {
            area += PolygonPoints[i].x * PolygonPoints[i + 1].y - PolygonPoints[i].y * PolygonPoints[i + 1].x;
            circumference += Math.Abs(PolygonPoints[i].x - PolygonPoints[i + 1].x);
            circumference += Math.Abs(PolygonPoints[i].y - PolygonPoints[i + 1].y);
        }

        circumference = Math.Abs(circumference);
        area = Math.Abs(area / 2);
        long insidepoints = area + 1 - circumference / 2;
        Console.WriteLine(circumference + insidepoints);
    }
}