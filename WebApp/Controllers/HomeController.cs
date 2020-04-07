using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        BuildContext builds = new BuildContext();
        GameContext games = new GameContext();
      
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
        public Build Find_part(string type, int needed_rate)
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

        public RedirectResult HandleIndex()
        {
            Session["wset"] = Request.Form["wset"];
            Session["bset"] = Request.Form["bset"];
            Session["aset"] = Request.Form["aset"];
            return RedirectPermanent("/Home/About");
        }

        public ActionResult About()
        {
            string q = (string)Session["wset"];
            string gamename = "Witcher 3";
            Game game = InfoAboutGame(gamename, q);
            Build needed0 = Find_part("cpu", game.cpu_rate);
            Build needed1 = Find_part("gpu", game.gpu_rate);
            Build needed2 = Find_part("ram", game.ram_rate);
            
            ViewBag.Cpu = needed0.ToString();
            ViewBag.Gpu = needed1.ToString();
            ViewBag.Ram = needed2.ToString();
            ViewBag.Total = "Total" + Convert.ToString(needed2.Price + needed1.Price + needed0.Price);

            return View();
        }

        
    }
}