using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string name { get; set; }
        
        public string set { get; set; }
        public int cpu_rate { get; set; }
        public int gpu_rate { get; set; }
        public int ram_rate { get; set; }
    }
}