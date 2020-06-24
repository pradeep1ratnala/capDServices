using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ResourcesController : ApiController
    {
        private newdatabase9Entities db = new newdatabase9Entities();

        // GET: api/Resources
        public IQueryable<Resource> GetResources()
        {
            return db.Resources;
        }

        // GET: api/Resources/5
        [ResponseType(typeof(Resource))]
        public IHttpActionResult GetResource(int id)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return NotFound();
            }

            return Ok(resource);
        }

        // PUT: api/Resources/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResource(int id, Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resource.Resource_ID)
            {
                return BadRequest();
            }

            db.Entry(resource).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
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

        // POST: api/Resources
        [ResponseType(typeof(Resource))]
        public IHttpActionResult PostResource(Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resources.Add(resource);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResourceExists(resource.Resource_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = resource.Resource_ID }, resource);
        }

        // DELETE: api/Resources/5
        [ResponseType(typeof(Resource))]
        public IHttpActionResult DeleteResource(int id)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return NotFound();
            }

            db.Resources.Remove(resource);
            db.SaveChanges();

            return Ok(resource);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResourceExists(int id)
        {
            return db.Resources.Count(e => e.Resource_ID == id) > 0;
        }
    }
}