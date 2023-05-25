using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tärning tärning = new Tärning();

            while (true)
            {
                Console.WriteLine("Välkommen till tärningskastarprogramet! \r\nSkriv in spelare 1 följt av spelare 2 och sen kommer ni få ett poäng beroende på tärningsvärdet");
                Console.Write("Namn 1:");
                string namn1 = Console.ReadLine();
                Console.Write("Namn 2:");
                string namn2 = Console.ReadLine();
                int värde = tärning.Kasta();
                Console.WriteLine($"{namn1} fick värdet " + värde);
                int värde2 = tärning.Kasta();
                Console.WriteLine($"{namn2} fick värde " + värde2);
                if (värde == värde2)
                {
                    Console.WriteLine("Det blev lika");
                }
                else if (värde > värde2)
                {
                    Console.WriteLine($"{namn1} Vann");
                }
                else
                {
                    Console.WriteLine($"{namn2} Vann");
                }
                Console.WriteLine("Spela igen? (Ja/Nej)");
                string val = Console.ReadLine();
                if (val.ToLower() == "ja")
                {
                    Console.Clear();
                }
                else
                {
                    break;                   
                }
            }
        }

        class Tärning
        {
            //Medlemsvariabler
            int värde;
            Random rnd = new Random();
            //Metoder      
            public Tärning()
            {
                värde = 1;
            }
            public int Kasta()
            {
                return värde = rnd.Next(1, 7);
            }
            public int Värde
            {
                get { return värde; }
            }
        }
    }
}
