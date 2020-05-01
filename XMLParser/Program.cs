using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLParser
{
    class Part
    {
        string name;
        double price;

        public Part(string name1, double price1)
        {
            name = name1;
            price = price1;
        }

        public Part()
        {

        }
    }


    class Program
    {
        static string ConvertCPU(string q)
        {
            string result = "";

            var lst = q.Split(' ');
            List<string> lst1 = new List<string>();

            foreach (var word in lst)
            {
                lst1.Add(word);
            }

            while (lst1[0] != "Intel" && lst1[0] != "AMD")
            {
                lst1.RemoveAt(0);
            }

            List<string> key_words = new List<string>() { "Xeon", "Core", "Gold", "Bronze", "Silver", "Ryzen", "Athlon", "Pentium", "Celeron", "A6", "A8", "A12", "EPYC" };

            bool flag = false;
            bool flag_ryzen = false;
            bool flag_exit = false;

            foreach (var word in lst1)
            {
                result += word + " ";
                if (flag == true)
                {
                    if (flag_ryzen == false || flag_exit == true)
                    {
                        break;
                    }
                    flag_exit = true;
                }

                if (key_words.Contains(word))
                {
                    if (word == "Ryzen")
                    {
                        flag_ryzen = true;
                    }
                    flag = true;
                }

            }

            result = result.ToLower();
            result = result.Replace(" ", String.Empty);
            result = result.Replace("-", String.Empty);

            return result;
        }

        static string ConvertGPU(string q)
        {
            string result = "";

            q = q.ToLower();

            q = q.Replace("<", "");
            q = q.Replace(">", "");

            var lst = q.Split(' ');
            List<string> lst1 = new List<string>();

            foreach (var word in lst)
            {
                lst1.Add(word);
            }

            if (lst.Contains("geforce"))
            {
                int index = lst1.IndexOf("geforce");
                result += lst1[index] + lst1[index + 1];
                try
                {
                    if (lst1[index + 2] == "super")
                    {
                        result += lst1[index + 2];
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                int index = lst1.IndexOf("radeon");
                result += lst1[index] + lst1[index + 1];
                try
                {
                    if (lst1[index + 1] == "rx")
                    {
                        result += lst1[index + 2];
                    }

                }
                catch (Exception ex)
                {

                }
            }

            result = result.Replace(" ", "");

            return result;
        }

        static void Main(string[] args)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Nix.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            List<Part> parts = new List<Part>();

            

            foreach (XmlElement xnode in xRoot)
            {
                if (xnode.Attributes.GetNamedItem("Name").Value.Contains("Процессор"))
                {
                    foreach (XmlElement part in xnode.ChildNodes)
                    {
                        XmlNode attr1 = part.Attributes.GetNamedItem("Name");
                        XmlNode attr2 = part.Attributes.GetNamedItem("RUR");

                        try
                        {
                            string output = ConvertCPU(attr1.Value);
                            if (output != "")
                            {
                                using (StreamWriter sw = new StreamWriter("cpu.csv", true))
                                {
                                    sw.WriteLine(output);
                                }
                                using (StreamWriter sw = new StreamWriter("cpu_prices.csv", true))
                                {
                                    sw.WriteLine(attr2.Value);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error");
                        }
                    }
                }
                else if (xnode.Attributes.GetNamedItem("Name").Value.Contains("Видеокарт"))
                {
                    foreach (XmlElement part in xnode.ChildNodes)
                    {
                        XmlNode attr1 = part.Attributes.GetNamedItem("Name");
                        XmlNode attr2 = part.Attributes.GetNamedItem("RUR");

                        try
                        {
                            string output = ConvertGPU(attr1.Value);
                            if (output != "")
                            {
                                using (StreamWriter sw = new StreamWriter("gpu.csv", true))
                                {
                                    sw.WriteLine(output);
                                }
                                using (StreamWriter sw = new StreamWriter("gpu_prices.csv", true))
                                {
                                    sw.WriteLine(attr2.Value);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Error! " + attr1.Value);
                        }
                    }
                }
            }
            Console.Read();
        }
    }
}
