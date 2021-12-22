using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_ps5_6
{
    public static class Modnisia
    {
        public static void Start()
        {
            //Inicjacja pliku tekstowego zawierającego niezbędne informacje związane z zadaniem
            StreamReader sr = new StreamReader("..\\..\\..\\In0302.txt");
            string[] input;
            input = sr.ReadLine().Split(" ");

            int iloscUbran = Int32.Parse(input[0]);
            int udzwig = Int32.Parse(input[1]);
            Ubranie[] ubrania = new Ubranie[iloscUbran];
            int wskaznikUbrania;
            Ubranie ubranie;

            for (int i = 0; i < iloscUbran; i++)
            {
                input = sr.ReadLine().Split(" ");
                ubrania[i] = new Ubranie(Int32.Parse(input[0]), Int32.Parse(input[1]));
            }

            //Inicjalizacja tablic
            int[,] tablica = new int[iloscUbran + 1, udzwig + 1];
            int[,] tablicaWziecia = new int[iloscUbran + 1, udzwig + 1];
            tablicaWziecia = FillTablicaWithZeros(tablicaWziecia, iloscUbran + 1, udzwig + 1);

            //Generacja tablicy wartości i udźwigu
            for(int p = 0; p <= iloscUbran; p++)
            {
                for(int w = 0; w <= udzwig; w++)
                {
                    if (p == 0 || w == 0)
                    {
                        tablica[p, w] = 0; //pierwszy wiersz i pierwsza kolumna powinny być zerami
                        continue;
                    }
                    wskaznikUbrania = p - 1;
                    ubranie = ubrania[wskaznikUbrania];
                    if(ubranie.Waga <= w)
                    {
                        //sprawdzmy czy wzięcie kolejnego ubrania (pierwsza iteracja opłaci się zawsze - po to pierwszy wiersz i kolumna to 0) opłaca się
                        tablica[p, w] = Math.Max(ubranie.Cena + tablica[p - 1, w - ubranie.Waga], tablica[p - 1, w]);
                        if(tablica[p,w] == ubranie.Cena + tablica[p - 1, w - ubranie.Waga])
                        {
                            tablicaWziecia[p, w] = 1;
                        }
                    }
                    else
                    {
                        //jeśli się nie mieści to trzeba przepisać to co było wcześniej (bierzemy ostatnie ubranie)
                        tablica[p, w] = tablica[p - 1, w];
                    }
                }
            }
            int maksymalnaWartoscPlecaka = tablica[iloscUbran, udzwig];

            //pokazanie tablic
            PokazTablice(tablica, tablicaWziecia, udzwig, iloscUbran);

            //ustalenie które przedmioty musimy wziąć
            ZnajdzPoczatkiRozwiazan(tablica, tablicaWziecia, udzwig, iloscUbran, maksymalnaWartoscPlecaka, ubrania);
        }

        private static void ZnajdzPoczatkiRozwiazan(int[,] tablica, int[,] tablicaWziecia, int udzwig, int iloscUbran, int maksymalnaWartoscPlecaka, Ubranie[] ubrania)
        {
            List<RozwiazanieHelper> poczatkiRozwiazan = new List<RozwiazanieHelper>();
            for (int y = 0; y <= udzwig; y++)
            {
                for (int x = 0; x <= iloscUbran; x++)
                {
                    if(tablica[x,y] == maksymalnaWartoscPlecaka && tablicaWziecia[x,y] == 1)
                    {
                        poczatkiRozwiazan.Add(new RozwiazanieHelper(x, y));
                    }
                }
            }
            foreach(var item in poczatkiRozwiazan)
            {
                var stringListaUbran = "";
                var listaUbran = PodajRozwiazanie(item.x, item.y, tablica, tablicaWziecia, ubrania);
                foreach(var nrUbrania in listaUbran)
                {
                    stringListaUbran += $"{nrUbrania} ";
                }
                FileHelper.WriteLineToTxt($"{stringListaUbran} ","Out0302");
            }
        }

        private static List<int> PodajRozwiazanie(int x, int y, int[,] tablica, int[,] tablicaWziecia, Ubranie[] ubrania)
        {
            int xPointer = x;
            int yPointer = y;
            List<int> ktoreUbrania = new List<int>();
            int wagaAktualnegoPrzedmiotu;

            while (yPointer != 0 || xPointer != 0 && tablica[xPointer, yPointer] != 0)
            {
                ktoreUbrania.Add(xPointer);
                wagaAktualnegoPrzedmiotu = ubrania[xPointer-1].Waga;
                yPointer = yPointer - wagaAktualnegoPrzedmiotu;
                while(tablicaWziecia[xPointer, yPointer] != 1 && tablica[xPointer, yPointer] != 0)
                {
                    xPointer--;
                }
            }
            return ktoreUbrania;
        }

        private static void PokazTablice(int[,] tablica, int[,] tablicaWziecia, int udzwig, int iloscUbran)
        {
            for (int y = 0; y <= udzwig; y++)
            {
                for (int x = 0; x <= iloscUbran; x++)
                {
                    Console.Write(tablica[x, y] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
            for (int y = 0; y <= udzwig; y++)
            {
                for (int x = 0; x <= iloscUbran; x++)
                {
                    Console.Write(tablicaWziecia[x, y] + " ");
                }
                Console.Write("\n");
            }
        }

        private static int[,] FillTablicaWithZeros(int[,] tablicaWziecia, int v1, int v2)
        {
            for(int y = 0; y < v2; y++)
            {
                for (int x = 0; x < v1; x++)
                {
                    tablicaWziecia[x, y] = 0;
                }
            }
            return tablicaWziecia;
        }
    }
    public class Ubranie
    {
        public int Cena { get; set; }
        public int Waga { get; set; }
        public Ubranie(int cena, int waga)
        {
            Cena = cena;
            Waga = waga;
        }
    }

    public class RozwiazanieHelper
    {
        public int x { get; set; }
        public int y { get; set; }

        public RozwiazanieHelper(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
