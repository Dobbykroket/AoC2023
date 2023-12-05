using System.Numerics;
using AoC2023.tools;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace AoC2023.days.day_5;

public class Day5: Day
{
    private struct map
    {
        public long source;
        public long destination;
        public long length;
        public long delta;

        public map(long destination, long source, long length)
        {
            this.source = source;
            this.destination = destination;
            this.length = length;
            this.delta = destination - source;
        }
    }

    private long checkmap(List<map> maps, long target)
    {
        foreach (map m in maps)
        {
            if (target >= m.source && target < (m.source + m.length))
            {
                return (target + m.delta);
            }
        }

        return target;
    }
    public Day5()
    {
        this.Directory = "day 5";
    }
    public override void Run()
    {
        List<long> seeds = new List<long>();
        List<map> soilmap = new List<map>();
        List<map> fertilizermap = new List<map>();
        List<map> watermap = new List<map>();
        List<map> lightmap = new List<map>();
        List<map> temperaturemap = new List<map>();
        List<map> humiditymap = new List<map>();
        List<map> locationmap = new List<map>();
        List<map> currentmap = new List<map>();
        StreamReader data = LoadData();
        string? line;
        long? lowestlocation = null;
        while ((line = data.ReadLine()) != null)
        {
            if (line.Equals(""))
            {
                continue;
            }

            if (!char.IsDigit(char.Parse(line.Substring(0, 1))))
            {
                switch (line.Split(":")[0].Split("-")[0])
                {
                    case ("seeds"):
                    {
                        seeds = line.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                        continue;
                    }
                    case ("seed"):
                    {
                        currentmap = soilmap;
                        continue;
                    }
                    case ("soil"):
                    {
                        currentmap = fertilizermap;
                        continue;
                    }
                    case ("fertilizer"):
                    {
                        currentmap = watermap;
                        continue;
                    }
                    case ("water"):
                    {
                        currentmap = lightmap;
                        continue;
                    }
                    case ("light"):
                    {
                        currentmap = temperaturemap;
                        continue;
                    }
                    case ("temperature"):
                    {
                        currentmap = humiditymap;
                        continue;
                    }
                    case ("humidity"):
                    {
                        currentmap = locationmap;
                        continue;
                    }
                }
            }
            
            long[] ints = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            currentmap.Add(new map(ints[0], ints[1], ints[2]));
        }

        for (int i = 0; i < seeds.Count; i += 2)
        {
            for (long seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
            {
                long soil = checkmap(soilmap, seed);
                long fertilizer = checkmap(fertilizermap, soil);
                long water = checkmap(watermap, fertilizer);
                long light = checkmap(lightmap, water);
                long temperature = checkmap(temperaturemap, light);
                long humidity = checkmap(humiditymap, temperature);
                long location = checkmap(locationmap, humidity);

                if (lowestlocation is null || location < lowestlocation)
                {
                    lowestlocation = location;
                }

            }
        }

        Console.WriteLine(lowestlocation);
    }
}