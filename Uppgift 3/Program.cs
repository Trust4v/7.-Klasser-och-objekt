using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_3
{

    class Program
    {

        static void Main(string[] args)
        {
            
            int[] kort1A = new int[2];
            int[] kort2A = new int[2];
            Console.WriteLine("Välkommen till Dra kort pogram och tävling \r\nSkriv in två namn");
            Console.Write("Namn 1:");
            string namn1 = Console.ReadLine();
            Console.Write("Namn 2:");
            string namn2 = Console.ReadLine();
            Kort kort1 = new Kort();
            Kort kort2 = new Kort();
            kort1.DraKort();
            kort2.DraKort();
            for (int i = 0; i < 2; i++)
            {
                kort1A[i] = kort1.Värde[i];
            }
            for (int i = 0; i < 2; i++)
            {
                kort2A[i] = kort2.Värde[i];
            }
            bool storlek = kort1.TestaStorlek(kort1A, kort2A);
            if (storlek)
            {
                Console.WriteLine(namn1 + " vann med " + kort1.ToString());
                Console.WriteLine(namn2 + " hade " + kort2.ToString());
            }
            else
            {
                Console.WriteLine(namn2 + " vann med " + kort2.ToString());
                Console.WriteLine(namn1 + " hade " + kort1.ToString());
            }
        }
    }
    
    class Kort
    {
        //Medlems Variablerana
        int[] kort = new int[2] {1,1};
        public int speed = 1;
        public Random rnd = new Random(speed);
        //Metoder
        public void DraKort()
        {
            kort[0] = rnd.Next(1, 5);
            kort[1] = rnd.Next(1, 14);
        }
        public Kort(int t)
        {
            speed = t;
        }
        public bool TestaStorlek(int[] kort1, int[] kort2)
        {
            bool StörreEllerMindre = false;
            if (kort1[1]>kort2[1])
            {
                StörreEllerMindre = true;
            }
            if (kort1[1]==kort2[1])
            {
                if (kort1[0] < kort2[0])
                {
                    StörreEllerMindre = true;
                }
            }
            return StörreEllerMindre;
        }
        public override string ToString()
        {
            if (kort[0] == 1)
            {
                return "Härter "+ kort[1]; 
            }
            if (kort[0] == 2)
            {
                return "Spader " + kort[1];
            }
            if (kort[0] == 3)
            {
                return "Ruter " + kort[1];
            }
            if (kort[0] == 4)
            {
                return "Klöver " + kort[1];
            }
            else
            {
                return "Dinmamma";
            }
        }
        public int[] Värde
        {
            get { return kort; }
        }

    }
}
