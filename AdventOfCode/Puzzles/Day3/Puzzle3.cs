using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Puzzle3
{
    public static int GetPowerConsumption(string path)
    {
        List<List<char>> rows = File.ReadAllLines(path)
            .Select(row => row.ToList()).ToList();


        int columnsNum = rows[0].Count;
        //string gammaRate = "";
        //string epsilonRate = "";

        int oxyGenRating = 1;
        int C02scrubberRating = 1;

        string OGRValue = CalculeOGRValue(rows, columnsNum);
        string C02SRValue = CalculeC02SRValue(rows, columnsNum);

        if (OGRValue != "")
        {
            oxyGenRating = Convert.ToInt32(OGRValue, 2);
        }

        if (C02SRValue != "")
        {
            C02scrubberRating = Convert.ToInt32(C02SRValue, 2);
        }

        #region Comentado
        //mirar solo primer bit de cada numero
        //si no se encuentra valor buscado, pasar al siguiente bit

        //bit criteria -> OxyGenRating, ver bit mas comun pos X y quedarse con los que tengan ese valor en esa posicion.

        //recorrido columnas
        /*for (int column = 0; column < columnsNum; column++)
        {
            int unos = 0;
            int ceros = 0;

            //recorrido filas
            for (int row = 0; row < rows.Count; row++)
            {
                char digit = rows[row][column];

                if (digit.Equals('1'))
                {
                    unos++;
                }
                else if (digit.Equals('0'))
                {
                    ceros++;
                }
            }

            if (unos >= ceros)
            {
                gammaRate += "1";
            }
            else if (ceros > unos)
            {
                gammaRate += "0";
            }
        }

        epsilonRate = ReverseGamma(gammaRate);*/

        //int powerConsumption = 0;//Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        #endregion

        return oxyGenRating * C02scrubberRating;
    }

    private static string CalculeOGRValue(List<List<char>> rows, int columnsNum)
    {
        for (int column = 0; column < columnsNum; column++)
        {
            int unos = 0;
            int ceros = 0;

            for (int row = 0; row < rows.Count; row++)
            {
                char digit = rows[row][column];

                if (digit.Equals('1'))
                {
                    unos++;
                }
                else if (digit.Equals('0'))
                {
                    ceros++;
                }
            }

            if (unos >= ceros)
            {
                rows = rows.Where(row => row[column].Equals('1')).ToList();
            }
            else if (ceros > unos)
            {
                rows = rows.Where(row => row[column].Equals('0')).ToList();
            }

            if (rows.Count == 1)
            {
                return string.Join("", rows[0]);
            }
        }
        return "";
    }

    private static string CalculeC02SRValue(List<List<char>> rows, int columnsNum)
    {
        for (int column = 0; column < columnsNum; column++)
        {
            int unos = 0;
            int ceros = 0;

            for (int row = 0; row < rows.Count; row++)
            {
                char digit = rows[row][column];

                if (digit.Equals('1'))
                {
                    unos++;
                }
                else if (digit.Equals('0'))
                {
                    ceros++;
                }
            }

            if (ceros <= unos)
            {
                rows = rows.Where(row => row[column].Equals('0')).ToList();
            }
            else if (ceros > unos)
            {
                rows = rows.Where(row => row[column].Equals('1')).ToList();
            }

            if (rows.Count == 1)
            {
                return string.Join("", rows[0]);
            }
        }
        return "";
    }

    private static string ReverseGamma(string number)
    {
        string res = "";

        foreach (char c in number)
        {
            if (c.Equals('1'))
            {
                res += "0";
            }
            else if (c.Equals('0'))
            {
                res += "1";
            }
        }

        return res;
    }
}