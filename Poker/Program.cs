using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Poker
{
    class Program
    {
        //Erik, detta program kommer skapa en filmmap i C:\\ med namnet "Namn" och kräver därför administrator för att funka.
        //Förslagsvis plockar du bort filen efter du är klar eftersom det bara tar upp plats.
        static void Main(string[] args)
        {
            #region Variabler
            KortLek kortlek = new KortLek(1);
            double spelaresBrahet;
            double dealersBrahet;
            int antalSpelare = 2;
            int bettadeChips = 0;
            int parPlusBet = 0;
            bool spela = false;
            bool baraFunka = false;
            Spelare Spelare1 = new Spelare("test");
            Spelare Dealer = new Spelare("Dealer");
            #endregion
            #region Sparnings Makapärer
            try
            {
                string text = @"  _               _     _       
 | |             | |   | |      
 | |     __ _  __| | __| | __ _ 
 | |    / _` |/ _` |/ _` |/ _` |
 | |___| (_| | (_| | (_| | (_| |
 |______\__,_|\__,_|\__,_|\__,_|
                                ";
                Console.WriteLine(text);
                List<string> namn = new List<string>();
                using (StreamReader reader = new StreamReader(@"C:\Namn\namn.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        namn.Add(line);
                    }
                }
                Console.WriteLine("De sparade namnen är:");
                for (int i = 0; i < namn.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) " + namn[i]);
                }
                Console.Write("Skriv det nummer som korresponderar med ditt namn eller skriv \"0\" för att skapa en ny save: ");
                int val2 = int.Parse(Console.ReadLine());
                if (val2 != 0)
                {
                    using (StreamReader reader = new StreamReader($"C:\\Namn\\{namn[val2 - 1]}.txt"))
                    {
                        Spelare1.SetNamn(namn[val2 - 1]);
                        Spelare1.SättInChips(int.Parse(reader.ReadLine()));
                    }
                    baraFunka = true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Directory.CreateDirectory(@"C:\Namn");
                if (!baraFunka)
                {
                    string text = @"   _____ _                     
  / ____| |                    
 | (___ | | ____ _ _ __   __ _ 
  \___ \| |/ / _` | '_ \ / _` |
  ____) |   < (_| | |_) | (_| |
 |_____/|_|\_\__,_| .__/ \__,_|
                  | |          
                  |_|  ";
                    Console.Clear();
                    Console.WriteLine(text);
                    Console.Write("Hej och välkommen till Tre Korts Poker, här är målet att få så mycket chips som möjligt för pengar är makt osv\r\nFör att starta skriv ditt namn här: ");
                    string namn = Console.ReadLine();
                    Spelare1.SetNamn(namn);
                    Spelare1.SättInChips(1000);
                    using (StreamWriter writer = new StreamWriter(@"C:\Namn\namn.txt", true))
                    {
                        writer.WriteLine(namn);
                    }
                }
            }  
            #endregion
            
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
                Console.WriteLine("Du har " + Spelare1.GetChips + " chips");
                if (Spelare1.GetChips <= 1)
                {
                    Console.WriteLine("Du har för lite chips och kan därmed inte spela längre");
                }
                Console.Write("1) Spela spelet\r\n2) Regler(bra om man inte spelat innan)\r\n3)Avsluta\r\n");
                try
                {
                    int val = int.Parse(Console.ReadLine());
                    Console.Clear();
                    if (val == 1 && Spelare1.GetChips > 1) 
                    {
                        while (true)
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

                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Du har " + Spelare1.GetChips + " chips");
                                    Console.WriteLine("Maxbet är " + Spelare1.GetChips / 2);
                                    Console.WriteLine("Vad vill du satsa?:");
                                    bettadeChips = int.Parse(Console.ReadLine());
                                    if (bettadeChips <= Spelare1.GetChips / 2)
                                    {
                                        Spelare1.BettaChips(bettadeChips);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("HÖRRÖ så mycket får du inte betta");
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Ogiltig inmatning");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Du har " + Spelare1.GetChips + " chips kvar");
                                    Console.WriteLine("Maximala parplus bettet är " + (Spelare1.GetChips - bettadeChips));
                                    parPlusBet = int.Parse(Console.ReadLine());
                                    if (parPlusBet <= Spelare1.GetChips - bettadeChips)
                                    {
                                        Spelare1.BettaParPlus(parPlusBet);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Hörrö du kan inte bett sååå mycket...");
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Ogiltig inmatning");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                            }
                            for (int i = 0; i < 3; i++)
                            {
                                Spelare1.DraKort(kortlek.DraKort());
                                Dealer.DraKort(kortlek.DraKort());
                            } // Delar ut kort                        
                            Spelare1.SoteraHand();
                            Console.WriteLine(Spelare1.ToString());
                            Console.WriteLine("----------\r\nDin \"pokerhand\":");
                            spelaresBrahet = Spelare1.HurBraHand();
                            Console.WriteLine("Vill du spela handen? (Ja/Nej)");
                            while (true)
                            {
                                spela = false;
                                string val2 = Console.ReadLine();
                                if (val2.ToLower() == "ja")
                                {
                                    spela = true;
                                    Spelare1.SpelaHand();
                                }
                                break;
                            }
                            Dealer.SoteraHand();
                            Console.WriteLine(Dealer.ToString());
                            dealersBrahet = Dealer.HurBraHand();
                            #region Vinnst/Förlust Check
                            if (dealersBrahet < 12 && spela)
                            {
                                Console.WriteLine("Dealern har under D high och spelar inte");
                                Spelare1.SättInChips(bettadeChips * 4 + parPlusBet * Spelare1.ParPlusMulitplyer());

                            }
                            else if (dealersBrahet == spelaresBrahet && spela)
                            {
                                Console.WriteLine("Dealern och du har lika bra hand så det blir oavgjort");
                                Spelare1.SättInChips(bettadeChips * 2 + parPlusBet * Spelare1.ParPlusMulitplyer());
                            }
                            else if (dealersBrahet < spelaresBrahet && spela)
                            {
                                Console.WriteLine("Du har bättre kort än dealern så du vinner!");
                                Spelare1.SättInChips(bettadeChips * 4 + parPlusBet * Spelare1.ParPlusMulitplyer());
                            }
                            else if (spela)
                            {
                                Console.WriteLine("Du Förlorade...");
                                Spelare1.SättInChips(parPlusBet * Spelare1.ParPlusMulitplyer());
                            }
                            else
                            {
                                Console.WriteLine("Du spelade inte och förlorade ditt ursprungliga bett");
                                Spelare1.SättInChips(parPlusBet * Spelare1.ParPlusMulitplyer());
                            }
                            #endregion
                            Console.WriteLine("Du har nu " + Spelare1.GetChips + " chips");
                            Dealer.TömHand();
                            Spelare1.TömHand();
                            Console.WriteLine("Spela igen? (Ja/Nej)");
                            string temp = Console.ReadLine();
                            if (Spelare1.GetChips <= 1)
                            {
                                break;
                            }
                            else if (temp.ToLower() == "ja")
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                    } //Pokerspelet
                    if (val == 2) //Regler
                    {
                        Console.WriteLine(@"Välkommen till Gustavs trekorts poker. 
I detta spel kommer du få möjligheten att satsa dina hårt intjänade pengar på ett riggat spel.
Du kommer att få börja med att lägga ett bet innan dina kort. Sedan när du får se dina kort kommer du få välja att dubbla din insättning
för en chans att få se dealerns kort. Om du dock känner att dina kort är för dåliga kan du lägga handen men din först insättning går förlorad.
Om du väljer att spela och du har en bättre hand än dealern eller som dealers bästa kort är under drottning vinner du tillbaka dubbla din instättning. 

Olika händer förklaring från bäst till sämst:
Straight Flush : Att du har 3 kort i följd som också är av samma färg.
Tree of a kind : Att du har 3 kort av samma valör.
Straight : Att du har 3 kort i följd.
Flush : Att alla dina kort är av samma färg
Pair : Du har två kort som är av samma valör
High Cards : Har du ingen av överstående får du highcard på ditt bästa kort. Highcards prioriteras från 2-E där E är bäst.

Du kommer även få möjliheten att lägga pengar på par plus. Om din hand blir ett par eller bättre har du möjligheten att få på ditt 
sidbet och den betals ut även om du vinner eller förlorar handen. 

Multiplikations Sheet Par Plus:
Straight Flush : 40x
Three of a kind : 30x
Straight : 6x
Flush : 3x
Pair : 1x
High Cards : 0x

Lycka till. 

Klicka enter för att fortsätta");//Relger
                        Console.ReadLine();
                    } //Regler
                    if (val == 3)
                    {
                        StreamWriter sw = new StreamWriter($"C:\\Namn\\{Spelare1.GetNamn}.txt");
                        sw.WriteLine(Spelare1.GetChips);
                        sw.Close();
                        Console.WriteLine("Sparar...");
                        Thread.Sleep(1000);
                        break;
                    } //Avsluta                    
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
        bool ärUppvärd; //True/False
        //konstruktror
        public Kort(Färg f, int v, bool U)
        {
            valör = v;
            färg = f;
            ärUppvärd = U;
        }
        //Metoder
        private string ValörFix(int v)
        {
            if (v == 11)
            {
                return "Kn";
            }
            if (v == 12)
            {
                return "D ";
            }
            if (v == 13)
            {
                return "K ";
            }
            if (v == 14)
            {
                return "E ";
            }
            else
            if (v == 10)
            {
                return "10";
            }
            {
                return v.ToString() + " ";
            }
        }
        public void VändKort(bool b)
        {
            ärUppvärd = b;
        }
        public override string ToString()
        {
            if (ärUppvärd)
            {
                return färg + " " + ValörFix(valör);
            }
            else
            {
                return "*Nedåtvänd*";
            }
        }
        //Egenskaper
        public string Valör
        {
            get { return ValörFix(valör); }
        }
        public double ValörNummer
        {
            get { return valör; }
        }
        public Färg Färg
        {
            get { return färg; }
        }
        public bool ÄrUppvänd
        {
            get { return ärUppvärd; }
        }

    }
    class KortLek
    {
        //medlemsvariabler
        List<Kort> kortlek = new List<Kort>();
        int antalKortlekar = 1; //1-oändligt
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
                        Kort temp = new Kort((Färg)i, j, true);
                        kortlek.Add(temp);
                    }
                }
            }
        } // bygger kortlek med valfri antal kortleker
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
        } //Blandar om med de korten som i nuläget finns i kortleken
        public void BlandaOm()
        {
            kortlek.Clear();
            for (int y = 0; y < antalKortlekar; y++)
            {
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 2; j < 15; j++)
                    {
                        Kort temp = new Kort((Färg)i, j, true);
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
        } //Lägger till alla korten igen och blandar om
        public void SkrivUt()
        {
            foreach (Kort kort in kortlek)
            {
                Console.WriteLine(kort);
            }
        } //Skriver ut resterande kort i leken
        internal Kort DraKort()
        {
            kortlek.ElementAt(0).VändKort(true);
            Kort returVärde = kortlek.ElementAt(0);
            kortlek.RemoveAt(0);
            return returVärde;
        }
        internal Kort DraKortGömt()
        {
            kortlek.ElementAt(0).VändKort(false);
            Kort returVärde = kortlek.ElementAt(0);
            kortlek.RemoveAt(0);
            return returVärde;
        }
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
    class Spelare
    {
        //medlemsvariabler
        string namn; //"Gustav"
        List<Kort> hand = new List<Kort>();
        int chips = 0; //0-oändligt
        int bettadeChips = 0; //Senast bettade chips
        int parPlus = 0; //Parplus bet
        double brahet = 0; //2-19
        //konstruktror
        public Spelare(string n)
        {
            namn = n;
        }
        public void SetNamn(string n)
        {
            namn = n;
        }
        //Metoder med handen
        public void DraKort(Kort kort)
        {
            hand.Add(kort);
        }
        public void VändKort(int mängdenkort, bool håll)
        {
            if (mängdenkort == 0)
            {
                foreach (Kort kort in hand)
                {
                    kort.VändKort(håll);
                }
            }
            else
            {
                for (int i = 0; i < mängdenkort; i++)
                {
                    hand[i].VändKort(håll);
                }
            }
        }
        public void SoteraHand()
        {
            hand.Sort((item1, item2) =>
            {
                int enumComparison = item1.ValörNummer.CompareTo(item2.ValörNummer);

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
        public double HurBraHand()
        {
            if (hand[0].Färg == hand[1].Färg && hand[0].Färg == hand[2].Färg)
            {
                if (hand[0].ValörNummer + 1 == hand[1].ValörNummer && hand[0].ValörNummer + 2 == hand[2].ValörNummer)
                {
                    Console.WriteLine("Straight Flush");
                    brahet = 19 + (hand[hand.Count - 1].ValörNummer / 100);
                }
                else
                {
                    Console.WriteLine("Flush");
                    brahet = 16 + (hand[hand.Count - 1].ValörNummer / 100);
                }
            }
            else if (hand[0].ValörNummer == hand[1].ValörNummer && hand[0].ValörNummer == hand[2].ValörNummer)
            {
                Console.WriteLine("Three of a kind");
                brahet = 18 + (hand[hand.Count - 1].ValörNummer / 100);
            }
            else if (hand[0].ValörNummer + 1 == hand[1].ValörNummer && hand[0].ValörNummer + 2 == hand[2].ValörNummer)
            {
                Console.WriteLine("Straight");
                brahet = 17 + (hand[hand.Count - 1].ValörNummer / 100);
            }
            else if (hand[0].ValörNummer == hand[1].ValörNummer || hand[0].ValörNummer == hand[2].ValörNummer || hand[1].ValörNummer == hand[2].ValörNummer)
            {
                Console.WriteLine("Pair");
                brahet = 15 + (hand[hand.Count - 2].ValörNummer / 100);
            }
            else
            {
                Console.WriteLine(hand[2].Valör + " High");
                brahet = hand[2].ValörNummer;
            }
            return brahet;
        }
        public double HurBraHandHemlig()
        {
            double brahet;
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
                brahet = hand[2].ValörNummer;
            }
            return brahet;
        }
        public void TömHand()
        {
            hand.Clear();
        }
        //Metoder med chipsen
        public void SättInChips(int c)
        {
            chips += c;
        }
        public void BettaChips(int c)
        {
            if (chips / 2 >= c)
            {
                chips -= c;
                bettadeChips = c;
            }
            else
            {
                Console.WriteLine("Du har för lite chips för att spela så mycket");
            }
        }
        public void SpelaHand()
        {
            chips -= bettadeChips;
        }
        public void BettaParPlus(int c)
        {
            chips -= c;
            parPlus = c;
        }
        public int ParPlusMulitplyer()
        {

            int ParPlusMult = 0;
            //Straight Flush
            if (brahet >= 19 && brahet < 20 )
            {
                ParPlusMult = 40;
            }            
            //Three of a kind
            if (brahet >= 18 && brahet < 19)
            {
                ParPlusMult = 30;
            }
            //Straight
            if (brahet >= 17 && brahet < 18)
            {
                ParPlusMult = 6;
            }
            //Flush
            if (brahet >= 16 && brahet < 17)
            {
                ParPlusMult = 3;
            }
            //Pair
            if (brahet >= 15 && brahet<16)
            {
                ParPlusMult = 1;
            }
            return ParPlusMult;
        }
        public override string ToString()
        {
            string handkort = "";
            string mellandel = "";
            foreach (Kort kort in hand)
            {
                handkort += @".------.";
            }
            handkort += "\r\n";
            foreach (Kort kort in hand)
            {
                if (kort.ÄrUppvänd)
                {
                    handkort += @"|" + kort.Valör + @"--. |";
                }
                else
                {
                    handkort += @"|::::::|";
                }
            }
            handkort += "\r\n";
            foreach (Kort kort in hand)
            {
                if (kort.ÄrUppvänd)
                {
                    if ((int)kort.Färg == 1)
                    {
                        mellandel = @"| (\/) |";
                    }
                    if ((int)kort.Färg == 2)
                    {
                        mellandel = @"| :/\: |";
                    }
                    if ((int)kort.Färg == 3)
                    {
                        mellandel = @"| :/\: |";
                    }

                    if ((int)kort.Färg == 4)
                    {
                        mellandel = @"| :(): |";
                    }
                }
                else
                {
                    mellandel = @"|::::::|";
                }
                handkort += mellandel;
            }
            handkort += "\r\n";
            foreach (Kort kort in hand)
            {
                if (kort.ÄrUppvänd)
                {
                    if ((int)kort.Färg == 1)
                    {
                        mellandel = @"| :\/: |";
                    }
                    if ((int)kort.Färg == 2)
                    {
                        mellandel = @"| (__) |";
                    }
                    if ((int)kort.Färg == 3)
                    {
                        mellandel = @"| :\/: |";
                    }
                    if ((int)kort.Färg == 4)
                    {
                        mellandel = @"| ()() |";
                    }
                }
                else
                {
                    mellandel = @"|::::::|";
                }
                handkort += mellandel;
            }
            handkort += "\r\n";
            foreach (Kort kort in hand)
            {
                if (kort.ÄrUppvänd)
                {
                    handkort += @"| '--" + kort.Valör + @"|";
                }
                else
                {
                    handkort += @"|::::::|";
                }
            }
            handkort += "\r\n";
            foreach (Kort kort in hand)
            {
                handkort += @"`------'";
            }
            return handkort;
        }
        //Egenskaper
        public List<Kort> GetHand
        {
            get { return hand; }
        }
        public double GetChips
        {
            get { return chips; }
        }
        public string GetNamn
        {
            get { return namn; }
        }
    }
}