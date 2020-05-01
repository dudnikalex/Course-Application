using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UploaderTest.Models;
using ParsingFramework;
using System.Xml;
using System.IO;

namespace UploaderTest.Controllers
{
    

    public class BuildsController : ApiController
    {


        private BuildContext db = new BuildContext();

        // GET: api/Builds
        public IQueryable<Build> GetBuilds()
        {
            string[] cpu = File.ReadAllLines(@"C:\Users\Max\Documents\GitHub\Course-Application\XMLParser\bin\debug\cpu.csv");
            string[] cpu_prices = File.ReadAllLines(@"C:\Users\Max\Documents\GitHub\Course-Application\XMLParser\bin\debug\cpu_prices.csv");

            string[] gpu = File.ReadAllLines(@"C:\Users\Max\Documents\GitHub\Course-Application\XMLParser\bin\debug\gpu.csv");
            string[] gpu_prices = File.ReadAllLines(@"C:\Users\Max\Documents\GitHub\Course-Application\XMLParser\bin\debug\gpu_prices.csv");

            List<Hashtable> q = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\cpu");
            foreach(var part in q)
            {

                Build b = new Build((string)part["company"], "cpu", (string)part["model"], Convert.ToSingle(part["bench"]));
                string adapted_model = b.ModelName.ToLower().Replace(" ", "").Replace("-", "");
                if (adapted_model.StartsWith("intel")) adapted_model = adapted_model.Replace("intel", "");
                if (adapted_model.StartsWith("amd")) adapted_model = adapted_model.Replace("amd", "");
                for (int i = 0; i < cpu.Length; i++)
                {
                    var check = cpu[i];
                    
                    if (check.StartsWith("intel")) check = check.Replace("intel", "");
                    if (check.StartsWith("amd")) check = check.Replace("amd", "");
                    if (adapted_model == check)
                    {
                        b.Price = (int)Convert.ToSingle(cpu_prices[i]);
                        break;
                    }
                }
                db.Builds.Add(b);
            }

            List<Hashtable> q1 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\gpu");
            foreach (var part in q1)
            {

                Build b = new Build((string)part["company"], "gpu", (string)part["model"], Convert.ToSingle(part["bench"]));
                string adapted_model = b.ModelName.ToLower().Replace(" ", "").Replace("-", "");
                if (adapted_model.StartsWith("nvidia")) adapted_model = adapted_model.Replace("nvidia", "");
                if (adapted_model.StartsWith("amd")) adapted_model = adapted_model.Replace("amd", "");
                if (adapted_model.StartsWith("sapphire")) adapted_model = adapted_model.Replace("sapphire", "");
                if (adapted_model.StartsWith("msi")) adapted_model = adapted_model.Replace("msi", "");
                if (adapted_model.StartsWith("gigabyte")) adapted_model = adapted_model.Replace("gigabyte", "");
                int id1 = adapted_model.IndexOf("8gb");
                if (id1 != -1) adapted_model.Remove(id1);
                int id2 = adapted_model.IndexOf("6gb");
                if (id2 != -1) adapted_model.Remove(id2);
                int id3 = adapted_model.IndexOf("4gb");
                if (id3 != -1) adapted_model.Remove(id3);
                int id4 = adapted_model.IndexOf("2gb");
                if (id4 != -1) adapted_model.Remove(id4);
                int id5 = adapted_model.IndexOf("11gb");
                if (id5 != -1) adapted_model.Remove(id5);

                for (int i = 0; i < gpu.Length; i++)
                {
                    var check = gpu[i];

                    if (check.StartsWith("geforce")) check = check.Replace("geforce", "");
                    if (check.StartsWith("radeon")) check = check.Replace("radeon", "");
                    if (adapted_model == check)
                    {
                        b.Price = (int)Convert.ToSingle(gpu_prices[i]);
                        break;
                    }
                }
                db.Builds.Add(b);
            }

            List<Hashtable> q2 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\ram");
            foreach (var part in q2)
            {
                Build b = new Build((string)part["company"], "ram", (string)part["model"], Convert.ToSingle(part["bench"]));
                db.Builds.Add(b);
            }

            List<Hashtable> q3 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\hdd");
            foreach (var part in q3)
            {
                Build b = new Build((string)part["company"], "hdd", (string)part["model"], Convert.ToSingle(part["bench"]));
                db.Builds.Add(b);
            }

            List<Hashtable> q4 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\ssd");
            foreach (var part in q4)
            {
                Build b = new Build((string)part["company"], "ssd", (string)part["model"], Convert.ToSingle(part["bench"]));
                db.Builds.Add(b);
            }

            
            db.SaveChanges();

            return db.Builds;
        }



        // GET: api/Builds/5
        [ResponseType(typeof(Build))]
        public IHttpActionResult GetBuild(int id)
        {
            Build build = db.Builds.Find(id);
            if (build == null)
            {
                return NotFound();
            }

            return Ok(build);
        }

        // PUT: api/Builds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuild(int id, Build build)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != build.Id)
            {
                return BadRequest();
            }

            db.Entry(build).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Builds
        [ResponseType(typeof(Build))]
        public IHttpActionResult PostBuild(Build build)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Builds.Add(build);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = build.Id }, build);
        }

        // DELETE: api/Builds/5
        [ResponseType(typeof(Build))]
        public IHttpActionResult DeleteBuild(int id)
        {
            Build build = db.Builds.Find(id);
            if (build == null)
            {
                return NotFound();
            }

            db.Builds.Remove(build);
            db.SaveChanges();

            return Ok(build);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildExists(int id)
        {
            return db.Builds.Count(e => e.Id == id) > 0;
        }
    }
}