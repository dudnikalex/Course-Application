using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Build : IComparable<Build>
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }
        public string ModelName { get; set; }
        public Single Rating { get; set; }
        public int Price { get; set; }

        public Build()
        {

        }
        public Build(string comp, string type, string modelname, Single rank) {
            Company = comp;
            Type = type;
            ModelName = modelname;
            Rating = rank;
            Price = 0;
        }

        public override string ToString()
        {
            return $"{Company} {ModelName}";
        }

        public int CompareTo(Build other)
        {
            if(Rating < other.Rating)
            {
                return -1;
            } 
            else
            {
                return 1;
            }
        }
    }
}