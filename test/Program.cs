using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pass the filepath and filename to the StreamWriter Constructor
            string temp = "";
            List<string> namn = new List<string>();
            try
            {
                
                using (StreamReader reader = new StreamReader("C:\\Test.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        namn.Add(line);
                    }
                    Console.WriteLine("De sparade namnen är:");
                    for (int i = 0; i < namn.Count; i++)
                    {
                        Console.WriteLine(namn[i] + $"({i+1})");
                    }
                }
                throw new Exception("Forcing an exception");
            }
            
        
            catch (Exception)
            {
                StreamWriter sw = new StreamWriter("C:\\Test.txt");
                //Write a line of text
                sw.WriteLine("Hello World!!");
                //Write a second line of text
                sw.WriteLine("From the StreamWriter class");
                //Close the file
                sw.Close();
            // Exception handling code in the catch block
                Console.WriteLine("Caught an exception: ");
            }
            
        }
    }
}
