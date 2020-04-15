using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParsingFramework;
using System.IO;

namespace UserbenchmarkParser
{
    class Program
    {
        private const int numOfUrls = 5;
        static void Main(string[] args)
        {
            
            string[] names = { "cpu", "gpu", "ssd", "hdd", "ram" };

            string[] urls = File.ReadAllLines(@"../../Sourse/urls.txt");

            for (int i = 0; i < numOfUrls; i++)
            {
                BenchParser.Download(urls[i], names[i]);
            }
            


        }
    }
}
