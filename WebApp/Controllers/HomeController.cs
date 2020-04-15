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

        List<string> list_of_games = new List<string>() { "WOW", "PUBG", "Dota 2", "Battlefield 5", "CS:GO", "ap", "ps", "Fortine", "Witcher 3"};

        public ActionResult Index()
        {
            games.Games.Add(new Game { name = "Witcher 3", set = "lg1080", cpu_rate = 40, gpu_rate = 21, ram_rate = 8, hdd_rate = 1, ssd_rate = 1 });
            games.SaveChanges();
            List<Hashtable> q = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\cpu");
            foreach(var part in q)
            {
                Build b = new Build( (string)part["company"], "cpu", (string)part["model"], Convert.ToSingle(part["bench"]) );
                builds.Builds.Add(b);
            }
            List<Hashtable> q1 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\gpu");
            foreach (var part in q1)
            {
                Build b = new Build((string)part["company"], "gpu", (string)part["model"], Convert.ToSingle(part["bench"]));
                builds.Builds.Add(b);
            }
            List<Hashtable> q2 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\hdd");
            foreach (var part in q2)
            {
                Build b = new Build((string)part["company"], "hdd", (string)part["model"], Convert.ToSingle(part["bench"]));
                builds.Builds.Add(b);
            }
            List<Hashtable> q3 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\ram");
            foreach (var part in q3)
            {
                Build b = new Build((string)part["company"], "ram", (string)part["model"], Convert.ToSingle(part["bench"]));
                builds.Builds.Add(b);
            }
            List<Hashtable> q4 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\ssd");
            foreach (var part in q4)
            {
                Build b = new Build((string)part["company"], "ssd", (string)part["model"], Convert.ToSingle(part["bench"]));
                builds.Builds.Add(b);
            }
            builds.SaveChanges();
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
        public Build Find_part(string type, Single needed_rate)
        {
            IEnumerable<Build> q = builds.Builds;
            Build val = new Build();
            float min_needed_rate = 1000;
            foreach(var part in q)
            {
                if(part.Company == type && part.Rating >= needed_rate && part.Rating < min_needed_rate)
                {
                    min_needed_rate = part.Rating;
                    val = part;
                }
            }
            return val;
        } 

        public ActionResult About()
        {
            Hashtable ht = new Hashtable();
            foreach(var app in list_of_games)
            {
                ht.Add(app, Request.Form[app]);
            }



            string gamename = "Witcher 3";
            Game game = InfoAboutGame(gamename, (string)ht[gamename]);
            Build needed0 = Find_part("cpu", game.cpu_rate);
            Build needed1 = Find_part("gpu", game.gpu_rate);
            Build needed2 = Find_part("ram", game.ram_rate);
            Build needed3 = Find_part("hdd", game.hdd_rate);
            Build needed4 = Find_part("ssd", game.ssd_rate);
            ViewBag.Cpu = needed0.ToString();
            ViewBag.Gpu = needed1.ToString();
            ViewBag.Ram = needed2.ToString();
            ViewBag.Hdd = needed3.ToString();
            ViewBag.Ssd = needed4.ToString();
            ViewBag.Total = "Total" + Convert.ToString(needed2.Price + needed1.Price + needed0.Price + needed3.Price + needed4.Price);
            
            return View();
        }

        
    }
}