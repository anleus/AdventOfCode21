using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

class Puzzle5
{
    public static int Part1(string path)
    {
        List<string> list = File.ReadAllLines(path).ToList();

        int[,] ocean = new int[1000, 1000];
        
        for (int index = 0; index < list.Count; index++)
        {
            string[] coords = list[index].Split(" -> ");
            string[] origin = coords[0].Split(",");
            string[] dest = coords[1].Split(",");

            int x1 = int.Parse(origin[0]);
            int y1 = int.Parse(origin[1]);

            int x2 = int.Parse(dest[0]);
            int y2 = int.Parse(dest[1]);

            if (x1 == x2)
            {
                for (int y = y1; (y1 < y2 && y <= y2) || (y1 > y2 && y >= y2); y = (y1 < y2)? y + 1 : y - 1)
                {
                    ocean[x1, y]++;
                }
            }
            else if (y1 == y2)
            {
                for (int x = x1; (x1 < x2 && x <= x2) || (x1 > x2 && x >= x2); x = (x1 < x2)? x + 1 : x - 1)
                {
                    ocean[x, y1]++;
                }
            }
        }

        int overlapLines = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                if (ocean[i, j] >= 2)
                {
                    overlapLines++;
                }
            }
        }

        return overlapLines;
    }

    public static int Part2(string path)
    {
        List<string> list = File.ReadAllLines(path).ToList();

        int[,] ocean = new int[1000, 1000];

        for (int index = 0; index < list.Count; index++)
        {
            string[] coords = list[index].Split(" -> ");
            string[] origin = coords[0].Split(",");
            string[] dest = coords[1].Split(",");

            int x1 = int.Parse(origin[0]);
            int y1 = int.Parse(origin[1]);

            int x2 = int.Parse(dest[0]);
            int y2 = int.Parse(dest[1]);

            if (x1 == x2)
            {
                for (int y = y1; (y1 < y2 && y <= y2) || (y1 > y2 && y >= y2); y = (y1 < y2) ? y + 1 : y - 1)
                {
                    ocean[x1, y]++;
                }
            }
            else if (y1 == y2)
            {
                for (int x = x1; (x1 < x2 && x <= x2) || (x1 > x2 && x >= x2); x = (x1 < x2) ? x + 1 : x - 1)
                {
                    ocean[x, y1]++;
                }
            }
            else if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
            {
                for (int x = x1, y = y1; 
                    ((x1 < x2 && x <= x2) || (x1 > x2 && x >= x2)) && ((y1 < y2 && y <= y2) || (y1 > y2 && y >= y2)); 
                    x = (x1 < x2) ? x + 1 : x - 1, y = (y1 < y2) ? y + 1 : y - 1)
                {
                    ocean[x, y]++;
                }
            }
        }

        int overlapLines = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                if (ocean[i, j] >= 2)
                {
                    overlapLines++;
                }
            }
        }

        return overlapLines;
    }
}
