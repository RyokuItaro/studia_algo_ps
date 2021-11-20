using System;
using System.IO;

namespace algorytmyps3_4
{
    public static class MergeSort
    {
        public static void Start()
        {
            int lengthOfArray = 0;
            int[] numbers = InitializeNumbers(ref lengthOfArray);
            string numbersTxt = "";
            MergeSortNumbers(ref numbers, 0, numbers.Length - 1);
            foreach (var item in numbers)
                numbersTxt += $"{item} ";
            FileSaver.WriteLineToTxt(numbersTxt, "Out0201");
        }

        private static int[] InitializeNumbers(ref int lengthOfArray)
        {
            StreamReader sr = new StreamReader("..\\..\\..\\In0201.txt");
            int length = Int32.Parse(sr.ReadLine());
            string[] numbersAsStrings = sr.ReadToEnd().Split(' ');
            int[] numbers = new int[length];
            
            for(int i = 0; i < length; i++)
            {
                numbers[i] = Int32.Parse(numbersAsStrings[i]);
            }

            sr.Close();

            lengthOfArray = length;
            return numbers;
        }
        private static void MergeSortNumbers(ref int[]numbers, int left, int right)
        {
            if(left < right) { 
                int middle = left + (right - left) / 2;
                MergeSortNumbers(ref numbers, left, middle); //pierwszą część podzieloną na pół sortujemy ponownie
                MergeSortNumbers(ref numbers, middle+1, right); //drugą to samo
                MergeArrays(ref numbers, left, middle, right); //na koniec trzeba je złączyć
            }
        }

        private static void MergeArrays(ref int[] numbers, int left, int middle, int right)
        {
            int i, j, k;
            int numbersOnLeftSide = middle - left + 1;
            int numbersOnRightSide = right - middle;
            int[] LeftSide = new int[numbersOnLeftSide];
            int[] RightSide = new int[numbersOnRightSide];

            for (i = 0; i < numbersOnLeftSide; i++) //dzielimy na lewą stronę
            {
                LeftSide[i] = numbers[left + i];
            }

            for (j = 0; j < numbersOnRightSide; j++) //dzielimy na prawą stronę
            {
                RightSide[j] = numbers[middle + 1 + j];
            }
            i = 0;
            j = 0;
            k = left;


            //Łączymy obie strony w jedno, powrównując liczby
            while(i < numbersOnLeftSide && j < numbersOnRightSide)
            {
                if (LeftSide[i] <= RightSide[j])
                    numbers[k] = LeftSide[i++];
                else
                    numbers[k] = RightSide[j++];
                k++;
            }

            while(i < numbersOnLeftSide)
                numbers[k++] = LeftSide[i++];
            while (j < numbersOnRightSide)
                numbers[k++] = RightSide[j++];
        }
    }
}
