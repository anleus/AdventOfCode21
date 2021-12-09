using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Puzzle8 
{
    public static int Part1(string path) 
    {
        string[] rows = File.ReadAllLines(path).ToArray();

        int simpleNumbers = 0;

        for (int index = 0; index < rows.Length; index++)
        {
            string[] values = rows[index].Split("|");
            List<string> numbers = values[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            numbers.ForEach(number => simpleNumbers += CheckSimple(number) ? 1 : 0);
        }

        return simpleNumbers;
    }

    /*
     *  aaaa
     * b    c
     * b    c
     *  dddd
     * e    f
     * e    f
     *  gggg
     *  
     */
    public static int Part2(string path)
    {
        string[] rows = File.ReadAllLines(path).ToArray();

        int totalSum = 0;

        for (int index = 0; index < rows.Length; index++)
        {
            //diccionario con significado combinaciones
            string[] decode = new string[10];

            //separacion de combinacion y numeros seleccionados
            string[] values = rows[index].Split("|");

            //separacion de combinaciones
            List<string> patterns = values[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            //seleccionar y añadir numeros conocidos a diccionario
            decode[1] = patterns.Where(chain => chain.Length == 2).First();
            decode[4] = patterns.Where(chain => chain.Length == 4).First();
            decode[7] = patterns.Where(chain => chain.Length == 3).First();
            decode[8] = patterns.Where(chain => chain.Length == 7).First();

            //elbow formed by BD
            char[] elbow = PatternDifference(decode[1], decode[4]);

            //joining 4 with 7 we have almost a 9 except for one
            char[] almostNine = PatternAddition(decode[4], decode[7]);

            //sacamos el 9, la diferencia con almostNine es de 1, mientras que con el resto de 6 es mayor
            decode[9] = patterns
                .Where(p => p.Length == 6)
                .Where(p => PatternDifference(new string(almostNine), p).Length == 1)
                .First();

            //sacamos el 5, el unico que tiene elbow de los de 5
            decode[5] = patterns
                .Where(p => p.Length == 5)
                .Where(p => HasAll(new string(elbow), p))
                .First();

            decode[3] = patterns
                .Where(p => p.Length == 5)
                .Where(p => PatternDifference(decode[5], p).Length == 1)
                .First();

            decode[2] = patterns
                .Where(p => p.Length == 5)
                .Where(p => p != decode[5] && p != decode[3])
                .First();

            char topRight = PatternDifference(decode[3], decode[5])[0];
            char topLeft = PatternDifference(decode[5], decode[3])[0];
            char middle = elbow.Where(c => c != topRight).First();

            decode[6] = patterns
                .Where(p => p.Length == 6)
                .Where(p => !HasAll(decode[1], p))
                .First();

            decode[0] = patterns
                .Where(p => p.Length == 6)
                .Where(p => !p.Contains(middle))
                .First();

            List<string> outputs = values[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            string res = "";

            foreach (string output in outputs)
            {
                for (int i = 0; i < decode.Length; i++)
                {
                    if (decode[i].Length == output.Length)
                    {
                        if (HasAll(decode[i], output))
                        {
                            res += i;
                        }

                        /*int same = decode[i].Where(c => output.Contains(c)).ToArray().Length;

                        if (same == output.Length)
                        {
                            res += i;
                        }*/
                    }
                }
            }

            totalSum += int.Parse(res);
        }

        return totalSum;
    }

    //buscamos que chars del segundo no estan en el primero
    private static char[] PatternDifference(string pat1, string pat2)
    {
        char[] pat1List = pat1.ToCharArray();
        char[] pat2List = pat2.ToCharArray();

        //seleccionar las de pat2 que no esten en pat1
        char[] diff = pat2List.Where(c => !pat1List.Contains(c)).ToArray();

        return diff;
    }

    private static bool HasAll(string part1, string part2)
    {
        bool hasAll = true;

        //parte chiquita
        char[] p1List = part1.ToCharArray();
        //parte grande
        char[] p2List = part2.ToCharArray();

        foreach (char c in p1List)
        {
            hasAll = hasAll && p2List.Contains(c) ? true : false;
        }

        return hasAll;
    }

    private static char[] PatternAddition(string pat1, string pat2)
    {
        string total = pat1 + pat2;
        char[] addition = total.ToCharArray().Distinct().ToArray();

        return addition;
    }

    private static bool CheckSimple(string number)
    {
        bool res = false;
        int chainLength = number.Length;

        if (chainLength == 2 || chainLength == 3 || chainLength == 4 || chainLength == 7)
        {
            res = true;
        }

        return res;
    }
}