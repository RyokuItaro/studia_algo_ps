using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmyps3_4
{
    public static class Kwadrat
    {
        public static void Start()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\In0206.txt");
            string[] input = sr.ReadToEnd().Split(' ');
            int length = Int32.Parse(input[0]);
            string word = input[1];
            int maxSideLettersLength = 4 * (length - 1);
            string maxWords = FillWords(word, maxSideLettersLength);
            char[,] square = new char[length, length];

            //Przydane do debugu (aby zobaczyć które pola nie zostały wypełnione)
            //for (int i = 0; i < length; i++)
            //{
            //    for (int j = 0; j < length; j++)
            //    {
            //        square[i, j] = '*';
            //    }
            //}

            GenerateSquare(ref square, length, maxWords);
            
            for (int i = 0; i < length; i++) {
                string line = "";
                for(int j = 0; j < length; j++)
                {
                    line += $"{square[i,j]} ";
                }
                FileSaver.WriteLineToTxt(line, "Out0206");
            }
        }

        private static string FillWords(string word, int maxSideLettersLength)
        {
            string filledString = "";
            for(int i = 0; i < maxSideLettersLength; i++)
            {
                filledString += word[i%(word.Length-1)];
            }
            return filledString;
        }

        private static void GenerateSquare(ref char[,] square, int length, string maxWords)
        {
            GenerateSides(ref square, maxWords, length, 0, 0);
        }

        private static void GenerateSides(ref char[,] square, string maxWords, int sideLength, int xCoordinate, int yCoordinate)
        {
            int x = xCoordinate;
            int y = yCoordinate;
            int letterCounter = 0;
            if (sideLength >= 1)
            {
                while (x < sideLength - 1)
                {
                    square[y, x++] = maxWords[letterCounter++];
                }
                while (y < sideLength - 1)
                {
                    square[y++, x] = maxWords[letterCounter++];
                }
                while (x > xCoordinate)
                {
                    square[y, x--] = maxWords[letterCounter++];
                }
                while (y > yCoordinate)
                {
                    square[y--, x] = maxWords[letterCounter++];
                }
                if(sideLength == 1)
                    square[yCoordinate / 2, xCoordinate / 2] = maxWords[0];
                GenerateSides(ref square, maxWords, sideLength - 1, xCoordinate + 1, yCoordinate + 1);
            }
        }
    }
}
