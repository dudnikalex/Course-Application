using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsingFramework
{
    public static class BenchParser
    {
        private static string path;
        private static string[] data;
        private static WebClient wc;

        static BenchParser()
        {
            wc = new WebClient();
            path = @"../../Files/";
        }

        public static void Download(string url, string name)
        {
            wc.DownloadFile(url, path + name);
        }

        public static Hashtable ParseString(string q)
        {
            q = q.Replace(',', ';');
            q = q.Replace('.', ',');
            string[] parts = q.Split(';');
            Hashtable result = new Hashtable();

            result.Add("company", (string)parts[2]);
            result.Add("model", (string)parts[3]);
            result.Add("bench", parts[5]);

            return result;
        }

        public static List<Hashtable> Parse(string path)
        {
            List<Hashtable> parsed_data = new List<Hashtable>();

            List<string> data = new List<string>();
            int q = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                   
                while ((line = sr.ReadLine()) != null)
                {
                    if (q != 0)
                        data.Add(line);
                    else
                        q++;
                }
            }
            
            foreach(var str in data)
            {
                parsed_data.Add(ParseString(str));
            }

            return parsed_data;
        } 
    }
}
