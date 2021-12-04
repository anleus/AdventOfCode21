using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle1
{
    public static int CalculeDepth(string path)
    {
        int increments = 0;

        List<int> values = File.ReadAllLines(path)
            .Where(row => row.Length > 0)
            .Select(int.Parse).ToList();

        for (int i = 0; i < values.Count - 3; i++)
        {
            int amount1 = values[i] + values[i + 1] + values[i + 2];
            int amount2 = values[i + 1] + values[i + 2] + values[i + 3];

            if (amount1 < amount2)
            {
                increments++;
            }
        }

        return increments;
    }
}
