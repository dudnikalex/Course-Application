using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploaderTest.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string name { get; set; }
        
        public string set { get; set; }
        public Single cpu_rate { get; set; }
        public Single gpu_rate { get; set; }
        public Single ram_rate { get; set; }
        public Single hdd_rate { get; set; }
        public Single ssd_rate { get; set; }
    }
}