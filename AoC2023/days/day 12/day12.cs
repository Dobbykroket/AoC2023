using System.Text;
using AoC2023.tools;

namespace AoC2023.days.day_12;

public class Day12: Day
{
    private static int recursivecalls = 0;
    private static int memosolves = 0;

    private static Dictionary<Tuple<string, int, int>, long> Possibilitymemo =
        new Dictionary<Tuple<string, int, int>, long>();
    private long RecursivePossibilityBuild(string corrupteddata, List<int> backupdata, int blanksquares)
    {
        recursivecalls++;
        
        var memokey = new Tuple<string, int, int>(corrupteddata, backupdata.Count, blanksquares);
        long shortcutvalue;
        if (Possibilitymemo.TryGetValue(memokey, out shortcutvalue))
        {
            memosolves++;
            return shortcutvalue;
        }
        
        // no more springs to add, check if any required springs left
        if (backupdata.Count == 0) 
        {
            if (corrupteddata.Any(c => c.Equals('#')))
            {
                Possibilitymemo.Add(memokey, 0);
                return 0;
            }

            Possibilitymemo.Add(memokey, 1);
            return 1;
        }
        
        // no more free blank spaces left, build rest of data and compare
        if (blanksquares == 0) 
        {
            StringBuilder dataBuilder = new StringBuilder();
            foreach (int length in backupdata)
            {
                char[] block = new char[length];
                Array.Fill(block, '#');
                dataBuilder.Append(block);
                dataBuilder.Append('.');
            }
            dataBuilder.Length--; // remove last char from StringBuilder
            string data = dataBuilder.ToString();
            
            for (int i = 0; i < data.Length; i++)
            {
                if (!(corrupteddata[i].Equals('?') || corrupteddata[i].Equals(data[i])))
                {
                    Possibilitymemo.Add(memokey, 0);
                    return 0;
                }
            }

            Possibilitymemo.Add(memokey, 1);
            return 1;
        }
        
        //quickfail if there are more required springs left than we have springs left to place
        if (backupdata.Sum() < corrupteddata.Count(c => c.Equals('#')))
        {
            Possibilitymemo.Add(memokey, 0);
            return 0;
        }
        
        // quickfail if we need to place more strings than we have valid placements left
        if (backupdata.Sum() > corrupteddata.Count(c => c.Equals('#') || c.Equals('?'))) 
        {
            Possibilitymemo.Add(memokey, 0);
            return 0;
        }

        long ifempty;
        long ifdatablock = 0;

        // assume next block is a blank space, fail if required spring, otherwise recurse
        if (corrupteddata[0].Equals('#'))
        {
            ifempty = 0;
        }
        else
        {
            ifempty = RecursivePossibilityBuild(corrupteddata.Substring(1, corrupteddata.Length - 1), backupdata,
                blanksquares - 1);
        }

        // assume next block is springs
        // test spring length for required blanks
        // check the next space after the block for a required spring, unless we're testing the end of the line
        if (!corrupteddata.Substring(0, backupdata[0]).Any(c => c.Equals('.')) 
            && (corrupteddata.Length == backupdata[0] || !corrupteddata[backupdata[0]].Equals('#')))
        {
            List<int> shorterBackupData = backupdata.ToList();
            shorterBackupData.Remove(shorterBackupData[0]);
            ifdatablock = RecursivePossibilityBuild(
                corrupteddata.Substring(backupdata[0] + 1, corrupteddata.Length - (backupdata[0] + 1)), shorterBackupData, blanksquares);
        }

        Possibilitymemo.Add(memokey, ifempty + ifdatablock);
        return ifempty + ifdatablock;
    }
    
    public Day12()
    {
        this.Directory = "day 12";
    }
    public override void Run()
    {
        long totalpossibillities = 0;
        StreamReader data = LoadData();
        string? line;
        while ((line = data.ReadLine()) != null)
        {
            string corrupteddata = line.Split(" ")[0];
            corrupteddata = string.Format("{0}?{0}?{0}?{0}?{0}", corrupteddata);
            int[] foldedbackupdata = line.Split(" ")[1].Split(",").Select(int.Parse).ToArray();
            List<int> backupdata = new List<int>();
            backupdata.AddRange(foldedbackupdata);
            backupdata.AddRange(foldedbackupdata);
            backupdata.AddRange(foldedbackupdata);
            backupdata.AddRange(foldedbackupdata);
            backupdata.AddRange(foldedbackupdata);
            int blanksquares = corrupteddata.Length - (backupdata.Count - 1) - backupdata.Sum();

            totalpossibillities += RecursivePossibilityBuild(corrupteddata, backupdata, blanksquares);
            Possibilitymemo.Clear();
        }
        Console.WriteLine(totalpossibillities);
        Console.WriteLine(recursivecalls);
        Console.WriteLine(memosolves);
    }
}