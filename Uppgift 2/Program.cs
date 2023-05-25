using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till bank. Skriv ditt namn för att skapa ett konto:");
            string namn = Console.ReadLine();
            BankKonto konto1 = new BankKonto(namn);

            while (true)
            {
                Console.WriteLine("Vad vill du göra? \r\n1)Sätt in pengar på konto\r\n2)Ta ut pengar\r\n3)Kolla pengar nivåer\r\n4)Avsluta");
                try
                {
                    int val = int.Parse(Console.ReadLine());
                    if (val==1)
                    {
                        Console.WriteLine("Hur mycket pengar vill du sätta in?");
                        int temp = int.Parse(Console.ReadLine());
                        konto1.SättaIn(temp);
                    }
                    else if (val ==2)
                    {
                        Console.WriteLine("Hur mycket pengar vill du ta ut?");
                        int temp = int.Parse(Console.ReadLine());
                        konto1.TaUt(temp);
                    }
                    else if (val == 3)
                    {
                        Console.WriteLine(konto1.ToString());
                        Console.ReadLine();
                    }
                    if (val == 4)
                    {
                        break;
                    }
                }
                catch
                {

                    throw;
                }
                Console.Clear();
            }
        }
    }
    class BankKonto
    {
        string kontoNamn;
        int pengar;
        public BankKonto(string n)
        {
            kontoNamn = n;
        }
        public void SättaIn(int p)
        {
            pengar += p;
        }
        public void TaUt(int p)
        {
            pengar -= p;
        }
        public override string ToString()

        {
            return "Kontot som ägs av " + kontoNamn + " har " + pengar + " kronor";
        }

    }
}
