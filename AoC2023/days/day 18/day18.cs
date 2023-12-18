using AoC2023.tools;

namespace AoC2023.days.day_18;

public class Day18: Day
{
    
    private struct Coordinate
    {
        public readonly long x;
        public readonly long y;

        public Coordinate(long x, long y)
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
            long distance = int.Parse(split[2][2..^2], System.Globalization.NumberStyles.HexNumber);
            switch (split[2][^2])
            {
                case '3': //U
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x, CurrentCoordinate.y + distance);
                    break;
                case '1': //D
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x, CurrentCoordinate.y - distance);
                    break;
                case '2': //L
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x - distance, CurrentCoordinate.y);
                    break;
                case '0': //R
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.x + distance, CurrentCoordinate.y);
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