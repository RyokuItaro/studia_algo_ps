using System;
using System.IO;

namespace algorytmyps3_4
{
    public static class Hanoi
    {
        public static void Start()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\In0204.txt");
            int amountOfDiscs = Int32.Parse(sr.ReadToEnd());
            char startPeg = '1'; // start tower in output
            char endPeg = '2'; // end tower in output
            char tempPeg = '3'; // temporary tower in output
            string stringToWrite = "";
            FileSaver.WriteLineToTxt($"N={amountOfDiscs}", "Out0204");
            solveTowers(amountOfDiscs, startPeg, endPeg, tempPeg, ref stringToWrite);
            FileSaver.WriteLineToTxt(stringToWrite, "Out0204");
        }

        private static void solveTowers(int n, char startPeg, char endPeg, char tempPeg, ref string stringToWrite)
        {
            if (n > 0)
            {
                solveTowers(n - 1, startPeg, tempPeg, endPeg, ref stringToWrite);
                stringToWrite += $"{startPeg}->{endPeg}, ";
                solveTowers(n - 1, tempPeg, endPeg, startPeg, ref stringToWrite);
            }
        }
    }
}
