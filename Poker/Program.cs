using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {

            KortLek kortlek = new KortLek(1);
            Spelare Gustav = new Spelare("Gustav");
            kortlek.Blanda();
            for (int i = 0; i < 5; i++)
            {
                Gustav.DraKort(kortlek.DraKort()); 
            }
            Gustav.KollaHand();
        }
        
    }

    enum Färg
    {
        Hjärter = 1,
        Spader = 2,
        Ruter = 3,
        Klöver = 4
    }
    class Kort
    {
        //Medlemsvariabler
        int valör; //2-E
        Färg färg; //"ruter"
        //Kompilator
        public Kort(Färg f, int v)
        {
            valör = v;
            färg = f;
        }
        private string ValörFix(int v)
        {            
            if (v == 11)
            {
                return "Kn";
            }
            if (v == 12)
            {
                return "D";
            }
            if (v == 13)
            {
                return "K";
            }
            if (v == 14)
            {
                return "E";
            }
            else
            {
                return v.ToString();
            }
        }
        public override string ToString()
        {
            return färg + " " + ValörFix(valör);
        }

    }
    class KortLek
    {
        //medlemsvariabler
        List<Kort> kortlek = new List<Kort>();
        int kortILeken=0;
        int antalKortlekar = 1;
        Random rnd = new Random();
        
        //konstruktor
        public KortLek(int ak)
        {
            antalKortlekar = ak;
            for (int y = 0; y < ak; y++)
            {
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 2; j < 15; j++)
                    {
                        Kort temp = new Kort((Färg)i, j);
                        kortlek.Add(temp);
                    }
                } 
            }
        }
        //Metoder
        public void Blanda()
        {
            kortILeken = 0;
            int n = kortlek.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Kort temp = kortlek[k];
                kortlek[k] = kortlek[n];
                kortlek[n] = temp;
            }
        }
        public void BlandaOm()
        {
            kortlek.Clear();
            for (int y = 0; y < antalKortlekar; y++)
            {
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 2; j < 15; j++)
                    {
                        Kort temp = new Kort((Färg)i, j);
                        kortlek.Add(temp);
                    }
                }
            }
            kortILeken = 0;
            int n = kortlek.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Kort temp = kortlek[k];
                kortlek[k] = kortlek[n];
                kortlek[n] = temp;
            }
        }
        public void SkrivUt()
        {
            foreach (Kort kort in kortlek)
            {
                Console.WriteLine(kort);
            }
        }
        internal Kort DraKort()
        {
            Kort returVärde = kortlek.ElementAt(0);
            kortlek.RemoveAt(0);
            return returVärde;
        }
        //ToString 
        public override string ToString()
        {
            return "Kortleken innehåller " + kortlek.Count + " kort just nu.";
        }

        

        //Egenskaper
        public Kort Tjuvkika
        {
            get { return kortlek[kortILeken-1]; }
        }
    }
    #region wip
    /*class AntalChips
{
   string kontoNamn;
   int pengar;
   public AntalChips(string n)
   {
       kontoNamn = n;
   }
   public void SättaIn(int p)
   {
       if (p < 0)
       {
           pengar += p;
       }
   }
   public void Betta(int p)
   {
       if (p < pengar)
       {
           pengar -= p;
       }
       else
       {
       }
   }
   public void KollaSaldo()
   {
       Console.WriteLine("Du har " + pengar + " chips");
   }
   public override string ToString()

   {
       return kontoNamn + " har " + pengar + " chips";
   }

}*/ 
    #endregion
    class Spelare
    {
        //medlemsvariabler
        string namn;
        List<Kort> hand = new List<Kort>();
        int chips;
        //konstruktro
        public Spelare(string n)
        {
            namn = n;
        }
        //Metoder
        public void DraKort(Kort kort)
        {
            hand.Add(kort);
        }
        public void KollaHand()
        {
            foreach (Kort kort in hand)
            {
                Console.Write(kort+ ", ");
            }
            Console.WriteLine();
        }

    }
}
