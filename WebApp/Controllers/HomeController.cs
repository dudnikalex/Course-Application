using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using ParsingFramework;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        BuildContext builds = new BuildContext();
        GameContext games = new GameContext();

        Build cpu_test;

        List<string> list_of_games = new List<string>() { "WOW", "PUBG", "Dota 2", "Battlefield 5", "CS:GO", "ap", "ps", "Fortnite", "Witcher 3" };

        public ActionResult Index()
        {
            Session["here"] = "True";
            return View();
        }

        public Game InfoAboutGame(string name, string set)
        {

            IEnumerable<Game> z = games.Games;
            Game needed_game = new Game();
            foreach (var game in z)
            {
                if (game.name.Trim() == name.Trim() && game.set.Trim() == set.Trim())
                {
                    needed_game = game;
                    break;
                }
            }

            return needed_game;
        }

        public void Choose_case()
        {
            if (Request.Form["ff"] == "atx")
            {
                ViewBag.Case = "Fractal Design Focus G";
                ViewBag.Case_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/cases/474488/";
                ViewBag.Case_price = 4300;
            }
            else if (Request.Form["ff"] == "mATX")
            {
                ViewBag.Case = "Fractal Design Core 100";
                ViewBag.Case_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/cases/858582/";
                ViewBag.Case_price = 0;
            }
            else if (Request.Form["ff"] == "itx")
            {
                ViewBag.Case = "Fractal Design Core 500";
                ViewBag.Case_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/cases/359243/";
                ViewBag.Case_price = 4710;
            }
            else
            {
                ViewBag.Case = "Fractal Design Focus G";
                ViewBag.Case_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/cases/474488/";
                ViewBag.Case_price = 4300;
            }
        }

        public void Choose_ssd()
        {
            ViewBag.Ssd = "Samsung EVO 860";
            ViewBag.Ssd_link = "https://www.samsung.com/ru/memory-storage/860-evo-sata-3-2-5-ssd/MZ-76E250BW/";
            ViewBag.Ssd_price = 4790;
        }

        public void Choose_hdd()
        {
            ViewBag.Hdd = "WD Black 1TB (Optional)";
            ViewBag.Hdd_link = "https://www.citilink.ru/catalog/computers_and_notebooks/hdd/hdd_in/860904/";
            ViewBag.Hdd_price = 5990;
        }

        public Build Find_part(string type, Single needed_rate, string pref)
        {
            if(pref == "nomatter" || pref == null)
            {
                pref = "amd";
            }
            IEnumerable<Build> q = builds.Builds;
            Build val = new Build();
            float min_needed_rate = 1000;
            foreach (var part in q)
            {
                if (part.Company.ToLower() == pref.ToLower() && part.Type == type && part.Rating >= needed_rate && part.Rating < min_needed_rate && part.Price > 0)
                {
                    if (type == "cpu" || type == "gpu")
                    {
                        bool flag = false;
                        if (part.ModelName.Contains("Xeon") || part.ModelName.Contains("Quadro") || part.ModelName.Contains("FirePro"))
                        {
                            continue;
                        }
                        foreach (var ch in part.ModelName)
                        {
                            if (ch == 'U' || ch == 'M' || ch == 'G' || ch == 'm')
                            {
                                flag = true;
                            }
                        }
                        if (flag == false)
                        {
                            min_needed_rate = part.Rating;
                            val = part;
                        }
                    }
                    else
                    {
                        min_needed_rate = part.Rating;
                        val = part;
                    }
                }
            }
            return val;
        }

        public void Choose_mb()
        {
            Build cpu = cpu_test;
            if (cpu.Company.ToLower() == "amd")
            {
                if (Request.Form["ff"] == "atx")
                {
                    ViewBag.MB = "Asus Prime B450-Plus";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1083557/";
                    ViewBag.MB_price = 7980;
                }
                else if (Request.Form["ff"] == "mATX")
                {
                    ViewBag.MB = "Gigabyte B450M S2H";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1120699/";
                    ViewBag.MB_price = 4690;
                }
                else if (Request.Form["ff"] == "itx")
                {
                    ViewBag.MB = "MSI-A320M-A PRO";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1196831/";
                    ViewBag.MB_price = 3670;
                }
                else
                {
                    ViewBag.MB = "Asus Prime B450-Plus";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1083557/";
                    ViewBag.MB_price = 7980;
                }
            }
            else if(cpu.ModelName[6] > '7' || cpu.ModelName[6] == '1')
            {
                if (Request.Form["ff"] == "atx")
                {
                    ViewBag.MB = "MSI B360 Gaming Plus";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1052396/";
                    ViewBag.MB_price = 7240;
                }
                else if (Request.Form["ff"] == "mATX")
                {
                    ViewBag.MB = "Asrock B365M-HDV";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1165861/";
                    ViewBag.MB_price = 4880;
                }
                else if (Request.Form["ff"] == "itx")
                {
                    ViewBag.MB = "Gigabyte GA-H110-D3A";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1138640/";
                    ViewBag.MB_price = 3160;
                }
                else
                {
                    ViewBag.MB = "MSI B360 Gaming Plus";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1052396/";
                    ViewBag.MB_price = 7240;
                }
            }
            else if(cpu.ModelName[6] > '5')
            {
                if (Request.Form["ff"] == "atx")
                {
                    ViewBag.MB = "Gigabyte GA-H110-D3A";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1086697/";
                    ViewBag.MB_price = 3160;
                }
                else if (Request.Form["ff"] == "mATX")
                {
                    ViewBag.MB = "Asus H110M-K";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/353574/";
                    ViewBag.MB_price = 4300;
                }
                else if (Request.Form["ff"] == "itx")
                {
                    ViewBag.MB = "Gigabyte GA-H110N";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/372483/";
                    ViewBag.MB_price = 5340;
                }
                if (Request.Form["ff"] == "atx")
                {
                    ViewBag.MB = "Gigabyte GA-H110-D3A";
                    ViewBag.MB_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/motherboards/1086697/";
                    ViewBag.MB_price = 3160;
                }    
            }
            
        }

        public void Choose_cpu_and_gpu()
        {
            List<Build> cpus = new List<Build>(),
            gpus = new List<Build>();

            foreach (var app in list_of_games)
            {
                string gamename = app;
                Game game = InfoAboutGame(gamename, (string)Request.Form[gamename]);
                cpus.Add(Find_part("cpu", game.cpu_rate, (string)Request.Form["mancpu"]));
                gpus.Add(Find_part("gpu", game.gpu_rate, (string)Request.Form["mangpu"]));
            }

            cpus.Sort();
            gpus.Sort();

            cpu_test = cpus.Last();

            ViewBag.Cpu = cpus.Last().ToString();
            ViewBag.Cpu_price = cpus.Last().Price.ToString();
            ViewBag.Gpu = gpus.Last().ToString();
            ViewBag.Gpu_price = gpus.Last().Price.ToString();

        }

        public void Choose_ram()
        {
            ViewBag.Ram = "Samsung DDR4 2666MHz RAM";
            ViewBag.Ram_link = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/memory/1075158/";
            ViewBag.Ram_price = 3250;
        }

        public void Count_price()
        {
            ViewBag.Total = "Total Price: " + Convert.ToString(Convert.ToInt32(ViewBag.Cpu_price) +
                                                               Convert.ToInt32(ViewBag.Gpu_price) +
                                                               Convert.ToInt32(ViewBag.Ram_price) +
                                                               Convert.ToInt32(ViewBag.MB_price) +
                                                               Convert.ToInt32(ViewBag.Hdd_price) +
                                                               Convert.ToInt32(ViewBag.Ssd_price) +
                                                               Convert.ToInt32(ViewBag.Case_price));
        }

        public ActionResult About()
        {
            Choose_case();
            Choose_cpu_and_gpu();
            Choose_hdd();
            Choose_ssd();
            Choose_mb();
            Choose_ram();
            Count_price();

            return View();
        }


    }
}