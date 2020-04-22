using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UploaderTest.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}