using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle6
{
    public const int NORMAL_FISH = 6;
    public const int NEWBORN_FISH = 8;
    public const int LIMIT_DAY1 = 8;
    public const int LIMIT_DAY2 = 256;

    public static int Part1(string path)
    {
        List<int> fishes = File.ReadLines(path)
            .FirstOrDefault()
            .Split(',')
            .Select(value => int.Parse(value)).ToList();

        //print de estado inicial

        for (int days = 0; days < LIMIT_DAY1; days++)
        {
            int nexFishes = 0;

            for (int i = 0; i < fishes.Count; i++)
            {
                if (fishes[i] == 0)
                {
                    fishes[i] = 6;
                    nexFishes++;
                }
                else
                {
                    fishes[i]--;
                }
            }

            for (int i = 0; i < nexFishes; i++)
            {
                fishes.Add(NEWBORN_FISH);
            }

            //print al final del dia cuando ya se ha restado y creado los bichos necesarios,
            //se les restara valor al dia siguiente de ser creados
        }

        return fishes.Count;
    }

    public static long Part2(string path)
    {
        List<int> fishes = File.ReadLines(path)
            .FirstOrDefault()
            .Split(',')
            .Select(value => int.Parse(value)).ToList();

        //print de estado inicial

        long[] agesCount = new long[NEWBORN_FISH + 1];
        fishes.ForEach(value => agesCount[value]++);

        for (int days = 0; days < LIMIT_DAY2; days++)
        {
            long fishesInZero = agesCount[0];

            //los que estan en la pos 1 no se mueven como el resto entre valores
            for (int i = 1; i < agesCount.Length; i++)
            {
                long countInAge = agesCount[i];
                agesCount[i - 1] = countInAge;
            }

            agesCount[NORMAL_FISH] += fishesInZero;
            agesCount[NEWBORN_FISH] = fishesInZero;
        }

        return agesCount.Sum(value => value);
    }
}
