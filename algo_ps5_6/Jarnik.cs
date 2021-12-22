using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_ps5_6
{
    public static class Jarnik
    {
        public static void Start()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\In0304.txt");
            string[] input;
            input = sr.ReadLine().Split(" ");
            int iloscWierzcholkow = Int32.Parse(input[0]);
            //Inicjalizacja krawedzi
            List<int> wierzcholkiMDR = new List<int>();
            List<Krawedz> krawedzieMDR = new List<Krawedz>();
            List<Krawedz> krawedzie = new List<Krawedz>();
            for(int i = 0; i < iloscWierzcholkow; i++)
            {
                input = sr.ReadLine().Split(" ");
                for(int j = 0; j < input.Length; j += 2)
                {
                    krawedzie.Add(new Krawedz(i+1, Int32.Parse(input[j]), Int32.Parse(input[j + 1])));
                }
            }
            Console.WriteLine($"NIEPOSORTOWANE KRAWEDZIE - {krawedzie.Count}");
            WypiszKrawedzie(krawedzie);
            MergeSortNumbers(ref krawedzie, 0, krawedzie.Count - 1);
            Console.WriteLine($"POSORTOWANE KRAWEDZIE - {krawedzie.Count}");
            WypiszKrawedzie(krawedzie);

            wierzcholkiMDR.Add(1); //wybieramy pierwszy wierzcholek od którego zaczniemy generowanie MDR
            while(wierzcholkiMDR.Count != iloscWierzcholkow)
            {
                for(int i = 0; i < krawedzie.Count; i++)
                {
                    if(wierzcholkiMDR.Contains(krawedzie[i].Poczatek) && !wierzcholkiMDR.Contains(krawedzie[i].Koniec))
                    {
                        krawedzieMDR.Add(krawedzie[i]);
                        wierzcholkiMDR.Add(krawedzie[i].Koniec);
                        i = 0;
                    }
                }
            }
            Console.WriteLine($"DRZEWO MDR - {krawedzieMDR.Count}");
            WypiszKrawedzieMDR(krawedzieMDR);
        }

        public static void WypiszKrawedzieMDR(List<Krawedz> krawedzieMDR)
        {
            string output = "";
            int wagaDrzewa = 0;
            foreach (var item in krawedzieMDR)
            {
                Console.Write($"({item.Poczatek},{item.Koniec})[{item.Waga}]\n");
                wagaDrzewa += item.Waga;
                output += $"{item.Poczatek} {item.Koniec} [{item.Waga}], ";
            }
            output = output.Remove(output.Length - 2);
            Console.Write(wagaDrzewa);
            FileHelper.WriteLineToTxt(output, "Out0304");
            FileHelper.WriteLineToTxt($"{wagaDrzewa}", "Out0304");
        }

        public static void WypiszKrawedzie(List<Krawedz> krawedzie)
        {
            foreach(var item in krawedzie)
            {
                Console.Write($"({item.Poczatek},{item.Koniec})[{item.Waga}]\n");
            }
        }

        public static void MergeSortNumbers(ref List<Krawedz> krawedzie, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSortNumbers(ref krawedzie, left, middle); //pierwszą część podzieloną na pół sortujemy ponownie
                MergeSortNumbers(ref krawedzie, middle + 1, right); //drugą to samo
                MergeArrays(ref krawedzie, left, middle, right); //na koniec trzeba je złączyć
            }
        }

        public static void MergeArrays(ref List<Krawedz> krawedzie, int left, int middle, int right)
        {
            int i, j, k;
            int numbersOnLeftSide = middle - left + 1;
            int numbersOnRightSide = right - middle;
            Krawedz[] LeftSide = new Krawedz[numbersOnLeftSide];
            Krawedz[] RightSide = new Krawedz[numbersOnRightSide];

            for (i = 0; i < numbersOnLeftSide; i++) //dzielimy na lewą stronę
            {
                LeftSide[i] = krawedzie[left + i];
            }

            for (j = 0; j < numbersOnRightSide; j++) //dzielimy na prawą stronę
            {
                RightSide[j] = krawedzie[middle + 1 + j];
            }
            i = 0;
            j = 0;
            k = left;


            //Łączymy obie strony w jedno, powrównując liczby
            while (i < numbersOnLeftSide && j < numbersOnRightSide)
            {
                if (LeftSide[i].Waga <= RightSide[j].Waga)
                    krawedzie[k] = LeftSide[i++];
                else
                    krawedzie[k] = RightSide[j++];
                k++;
            }

            while (i < numbersOnLeftSide)
                krawedzie[k++] = LeftSide[i++];
            while (j < numbersOnRightSide)
                krawedzie[k++] = RightSide[j++];
        }
    }

    public class Krawedz
    {
        public int Poczatek { get; set; }
        public int Koniec { get; set; }
        public int Waga { get; set; }
        public Krawedz(int poczatek, int koniec, int waga)
        {
            Poczatek = poczatek;
            Koniec = koniec;
            Waga = waga;
        }
    }
}
