using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        //ToDo:
        //Fixa bets
        //Ev fixa coolare kort med acci art
        //Pair plus
        //Fler spelare
        //Fråga erik om allt är wäk
        //
        static void Main(string[] args)
        {
            KortLek kortlek = new KortLek(1);
            Console.Write("Hej och välkommen till trekortsPoker\r\nFör att starta skriv ditt namn här:");
            Spelare Spelare1 = new Spelare(Console.ReadLine());
            Spelare Dealer = new Spelare("Dealer");
            int spelaresBrahet;
            int dealersBrahet;
            int antalSpelare = 2;
            while (true)
            {
                Console.Clear();
                string text = @"  _    _                       _                            
 | |  | |                     | |                           
 | |__| |_   ___   ___   _  __| |_ __ ___   ___ _ __  _   _ 
 |  __  | | | \ \ / / | | |/ _` | '_ ` _ \ / _ \ '_ \| | | |
 | |  | | |_| |\ V /| |_| | (_| | | | | | |  __/ | | | |_| |
 |_|  |_|\__,_| \_/  \__,_|\__,_|_| |_| |_|\___|_| |_|\__, |
                                                       __/ |
                                                      |___/ ";
                Console.WriteLine(text);
                Console.Write("1) Spela spelet\r\n2) Regler(bra om man inte spelat innan)");
                try
                {
                    int val = int.Parse(Console.ReadLine());
                    if (val == 1) //Pokerspelet
                    {
                        Console.Clear();
                        text = @"  _____      _             
 |  __ \    | |            
 | |__) |__ | | _____ _ __ 
 |  ___/ _ \| |/ / _ \ '__|
 | |  | (_) |   <  __/ |   
 |_|   \___/|_|\_\___|_|   
                           ";
                        Console.WriteLine(text);
                        #region blanda
                        if (kortlek.AntalKort < antalSpelare * 3)
                        {
                            kortlek.BlandaOm();
                        }
                        else
                        {
                            kortlek.Blanda();
                        }
                        #endregion
                        for (int i = 0; i < 3; i++)
                        {
                            Spelare1.DraKort(kortlek.DraKort());
                            Dealer.DraKort(kortlek.DraKort());
                        } // Delar ut kort                        
                        Spelare1.SoteraHand();
                        Console.WriteLine("Dina kort:");
                        Spelare1.KollaHand();
                        Console.WriteLine("----------\r\nDin \"pokerhand\":");
                        spelaresBrahet = Spelare1.HurBraHand();

                        Console.ReadLine();

                        dealersBrahet = Dealer.HurBraHand();
                        if (dealersBrahet < 12)
                        {
                            Console.WriteLine("Dealern har under D high och spelar inte");
                        }
                        else if (dealersBrahet == spelaresBrahet)
                        {
                            Console.WriteLine("Dealern och du har lika bra hand så det blir oavgjort");
                        }
                        else if (dealersBrahet < spelaresBrahet)
                        {
                            Console.WriteLine("Du har bättre kort än dealern så du vinner!");
                        }
                        else
                        {
                            Console.WriteLine("Du Förlorade...");
                        }
                        Console.ReadLine();
                    }
                    if (val == 2) //Regler
                    {

                    }
                    else
                    {

                    }
                }
                catch
                {
                    Console.WriteLine("Otillåten inmatning...");
                }
            }

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
        public string ValörS()
        {
            return ValörFix(valör);
        }
        public int Valör
        {
            get { return valör; }
        }
        public Färg Färg
        {
            get { return färg; }
        }

    }
    class KortLek
    {
        //medlemsvariabler
        List<Kort> kortlek = new List<Kort>();
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
            get { return kortlek[0]; }
        }
        public int AntalKort
        {
            get { return kortlek.Count; }
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
                Console.Write(kort + ", ");
            }
            Console.WriteLine();
        }
        public void SoteraHand()
        {
            hand.Sort((item1, item2) =>
            {
                int enumComparison = item1.Valör.CompareTo(item2.Valör);

                if (enumComparison != 0)
                {
                    return enumComparison;
                }
                else
                {
                    return item1.Färg.CompareTo(item2.Färg);
                }
            }); //Lånad från internet, över min lönegrad
        }
        public int HurBraHand()
        {
            int brahet;
            if (hand[0].Färg == hand[1].Färg && hand[0].Färg == hand[2].Färg)
            {
                if (hand[0].Valör + 1 == hand[1].Valör && hand[0].Valör + 2 == hand[2].Valör)
                {
                    Console.WriteLine("Straight Flush");
                    brahet = 19;
                }
                else
                {
                    Console.WriteLine("Flush");
                    brahet = 16;
                }
            }
            else if (hand[0].Valör == hand[1].Valör && hand[0].Valör == hand[2].Valör)
            {
                Console.WriteLine("Three of a kind");
                brahet = 18;
            }
            else if (hand[0].Valör + 1 == hand[1].Valör && hand[0].Valör + 2 == hand[2].Valör)
            {
                Console.WriteLine("Straight");
                brahet = 17;
            }
            else if (hand[0].Valör == hand[1].Valör || hand[0].Valör == hand[2].Valör || hand[1].Valör == hand[2].Valör)
            {
                Console.WriteLine("Pair");
                brahet = 15;
            }
            else
            {
                Console.WriteLine(hand[2].ValörS() + " High");
                brahet = hand[2].Valör;
            }
            return brahet;
        }
        public int HurBraHandHemlig()
        {
            int brahet;
            if (hand[0].Färg == hand[1].Färg && hand[0].Färg == hand[2].Färg)
            {
                if (hand[0].Valör + 1 == hand[1].Valör && hand[0].Valör + 2 == hand[2].Valör)
                {
                    brahet = 19;
                }
                else
                {
                    brahet = 16;
                }
            }
            else if (hand[0].Valör == hand[1].Valör && hand[0].Valör == hand[2].Valör)
            {
                brahet = 18;
            }
            else if (hand[0].Valör + 1 == hand[1].Valör && hand[0].Valör + 2 == hand[2].Valör)
            {
                brahet = 17;
            }
            else if (hand[0].Valör == hand[1].Valör || hand[0].Valör == hand[2].Valör || hand[1].Valör == hand[2].Valör)
            {
                brahet = 15;
            }
            else
            {
                brahet = hand[2].Valör;
            }
            return brahet;
        }
        public List<Kort> GetHand
        {
            get { return hand; }
        }

    }
}
