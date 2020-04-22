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
                if(part.Type == type && part.Rating >= needed_rate && part.Rating < min_needed_rate)
                {
                    min_needed_rate = part.Rating;
                    val = part;
                }
            }
            return val;
        } 

        public ActionResult About()
        {
            List<Build> cpus = new List<Build>(), 
                        gpus = new List<Build>(), 
                        rams = new List<Build>(), 
                        ssds = new List<Build>(), 
                        hdds = new List<Build>();

            foreach(var app in list_of_games)
            {
                string gamename = app;
                Game game = InfoAboutGame(gamename, (string)Request.Form[gamename]);
                cpus.Add(Find_part("cpu", game.cpu_rate));
                gpus.Add(Find_part("gpu", game.gpu_rate));
                rams.Add(Find_part("ram", game.ram_rate));
                hdds.Add(Find_part("hdd", game.hdd_rate));
                ssds.Add(Find_part("ssd", game.ssd_rate));
            }
            cpus.Sort();
            gpus.Sort();
            rams.Sort();
            hdds.Sort();
            ssds.Sort();

            ViewBag.Cpu = cpus.Last().ToString();
            ViewBag.Gpu = gpus.Last().ToString();
            ViewBag.Ram = rams.Last().ToString();
            ViewBag.Hdd = hdds.Last().ToString();
            ViewBag.Ssd = ssds.Last().ToString();
            ViewBag.Total = "Total" + Convert.ToString(gpus.Last().Price + cpus.Last().Price + rams.Last().Price + ssds.Last().Price + hdds.Last().Price);

            return View();
        }

        
    }
}