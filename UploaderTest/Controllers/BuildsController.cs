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

namespace UploaderTest.Controllers
{
    public class BuildsController : ApiController
    {
        private BuildContext db = new BuildContext();

        // GET: api/Builds
        public IQueryable<Build> GetBuilds()
        {
            List<Hashtable> q = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\cpu");
            foreach(var part in q)
            {
                Build b = new Build((string)part["company"], "cpu", (string)part["model"], Convert.ToSingle(part["bench"]));
                db.Builds.Add(b);
            }

            List<Hashtable> q1 = BenchParser.Parse(@"C:\Users\Max\Documents\GitHub\Course-Application\UserbenchmarkParser\Files\gpu");
            foreach (var part in q1)
            {
                Build b = new Build((string)part["company"], "gpu", (string)part["model"], Convert.ToSingle(part["bench"]));
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