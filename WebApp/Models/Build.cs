using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Build
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string ModelName { get; set; }
        public Single Rating { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Company} + {ModelName} + {Price}" + Environment.NewLine;
        }
    }
}