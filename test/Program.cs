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
            StreamWriter sw = new StreamWriter("C:\\Test.txt");
            //Write a line of text
            sw.WriteLine("Hello World!!");
            //Write a second line of text
            sw.WriteLine("From the StreamWriter class");
            //Close the file
            sw.Close();
        }
    }
}
