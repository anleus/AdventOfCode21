using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle7
{
    public static int Part1(string path)
    {
        List<int> input = File.ReadLines(path).FirstOrDefault().Split(",").Select(num => int.Parse(num)).ToList();
        input.Sort();

        int crabsAmount = input.Count;
        int medianaPos;

        if (crabsAmount % 2 == 0)
        {
            medianaPos = crabsAmount / 2;
        }
        else
        {
            medianaPos = (crabsAmount + 1) / 2;
        }

        return input.Sum(position => Math.Abs(position - input[medianaPos]));
    }

    public static int Part2(string path)
    {
        List<int> input = File.ReadLines(path).FirstOrDefault().Split(",").Select(num => int.Parse(num)).ToList();

        int crabsAmount = input.Count;
        int media = input.Sum(value => value) / crabsAmount;

        int fuelWasted = 0;

        for (int i = 0; i < input.Count; i++)
        {
            int difference = Math.Abs(input[i] - media);

            for (int j = 1; j <= difference; j++)
            {
                fuelWasted += j;
            }
        }

        return fuelWasted;
    }
}
