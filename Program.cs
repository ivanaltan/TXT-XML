using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TXT_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - XMLConvertor");
            Console.WriteLine("2 - XMLTemplateHelper");
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine(System.Environment.NewLine);

            if (c == '1')
            {
                XMLConvertor.Convert(@"C:\Users\Ivan.Altandziev\Downloads\PlatiValuta_test.tsv", @"C:\Users\Ivan.Altandziev\Downloads\PlatiValuta_test.xml",
                    true, @"2498092/20180122125214", @"2018-01-22T12:52:14", 3, 4.00, @"LEU IULIA/2830731090050");
                Console.ReadKey();
            }
            else if (c == '2')
            {
                Console.WriteLine("Location");
                string filein = Console.ReadLine();
                Console.WriteLine(System.Environment.NewLine);
                TXT_XML.XMLTemplateHelper.Do(filein);
            }
            else
            {
                Console.WriteLine("Wrong Input");
            }
        }


    }
}
