using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle2
{
    public static int CalculePosition(string path)
    {
        int horizontalPos = 0;
        int aim = 0;
        int depth = 0;

        List<Instruction> instructions = File.ReadAllLines(path)
            .Where(row => row.Length > 0)
            .Select(Instruction.ParseRow).ToList();

        foreach (Instruction ins in instructions)
        {
            switch(ins.Direction)
            {
                case "forward":
                    horizontalPos += ins.Amount;
                    depth += aim * ins.Amount;
                    break;
                case "up":
                    aim -= ins.Amount;
                    break;
                case "down":
                    aim += ins.Amount;
                    break;
            }
        }

        return horizontalPos * depth;
    }

    internal class Instruction
    {
        public string Direction;
        public int Amount;

        public Instruction(string direction, int amount)
        {
            Direction = direction;
            Amount = amount;
        }

        public static Instruction ParseRow(string row)
        {
            string[] values = row.Split(' ');

            return new Instruction(values[0], Int32.Parse(values[1]));
        }
    }

}

