using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle4
{
    public static int Part1(string path)
    {
        int[] drawedNums = File.ReadLines(path).FirstOrDefault().Split(',').Select(value => int.Parse(value)).ToArray();

        List<Carton> cartones = new List<Carton>();

        string[] input = File.ReadAllLines(path).Skip(2).ToArray();
        BingoNumber[,] matrix = new BingoNumber[5, 5];
        //Indice actual de la fila del carton
        int rowIndex = 0;

        foreach (string line in input)
        {
            if (String.IsNullOrEmpty(line))
            {
                BingoNumber[,] nuevo = new BingoNumber[5, 5];
                Array.Copy(matrix, nuevo, matrix.Length);

                cartones.Add(new Carton(nuevo));
                rowIndex = 0;
                Array.Clear(matrix, 0, matrix.Length);
            }
            else
            {
                List<string> row = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int columnIndex = 0; columnIndex < 5; columnIndex++)
                {
                    string value = row[columnIndex];
                    int num = int.Parse(value);

                    matrix[rowIndex, columnIndex] = new BingoNumber(num, false);
                }
                rowIndex++;
            } 
        }

        foreach (int num in drawedNums)
        {
            foreach (Carton carton in cartones)
            {
                if (carton.CheckNumber(num))
                {
                    return carton.CalculeFinalScore(num);
                }
            }
        }

        return 0;
    }

    public static int Part2(string path)
    {
        int[] drawedNums = File.ReadLines(path).FirstOrDefault().Split(',').Select(value => int.Parse(value)).ToArray();

        List<Carton> cartones = new List<Carton>();

        string[] input = File.ReadAllLines(path).Skip(2).ToArray();
        BingoNumber[,] matrix = new BingoNumber[5, 5];
        //Indice actual de la fila del carton
        int rowIndex = 0;

        foreach (string line in input)
        {
            if (String.IsNullOrEmpty(line))
            {
                BingoNumber[,] nuevo = new BingoNumber[5, 5];
                Array.Copy(matrix, nuevo, matrix.Length);

                cartones.Add(new Carton(nuevo));
                rowIndex = 0;
                Array.Clear(matrix, 0, matrix.Length);
            }
            else
            {
                List<string> row = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int columnIndex = 0; columnIndex < 5; columnIndex++)
                {
                    string value = row[columnIndex];
                    int num = int.Parse(value);

                    matrix[rowIndex, columnIndex] = new BingoNumber(num, false);
                }
                rowIndex++;
            }
        }

        Carton winner = null;
        int lastWinnerNumber = 0; ;
        foreach (int num in drawedNums)
        {
            foreach (Carton carton in cartones)
            {
                if (!carton.Winner && carton.CheckNumber(num))
                {
                    winner = carton;
                    lastWinnerNumber = num;
                    carton.Winner = true;
                }
            }
        }

        if (winner != null)
        {
            return winner.CalculeFinalScore(lastWinnerNumber);
        }
        else
        {
            return 0;
        }
    }

    internal class Carton
    {
        public BingoNumber[,] Numbers;
        public bool Winner;

        public Carton(BingoNumber[,] numbers)
        {
            Numbers = numbers;
            Winner = false;
        }

        public bool CheckNumber(int number)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Numbers[i,j].Number == number)
                    {
                        Numbers[i, j].IsMarked = true;

                        return CheckCombination();
                    }
                }
            }
            return false;
        }

        public bool CheckCombination()
        {
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                int checkRow = 0;
                int checkCol = 0;
                for (int j = 0; j < Numbers.GetLength(0); j++)
                {
                    if (Numbers[i,j].IsMarked)
                    {
                        checkRow++;
                    }

                    if (Numbers[j,i].IsMarked)
                    {
                        checkCol++;
                    }
                }

                if (checkRow == 5 || checkCol == 5)
                {
                    return true;
                }
            }
            return false;
        }

        public int CalculeFinalScore(int lastNum)
        {
            //usar agregate?
            int sum = 0;
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                for (int j = 0; j < Numbers.GetLength(0); j++)
                {
                    if (!Numbers[i, j].IsMarked)
                    {
                        sum += Numbers[i, j].Number;
                    }
                }
            }

            return sum * lastNum;
        }
    }

    internal class BingoNumber
    {
        public int Number;
        public bool IsMarked;

        public BingoNumber(int number, bool isMarked)
        {
            Number = number;
            IsMarked = isMarked;
        }
    }
}
