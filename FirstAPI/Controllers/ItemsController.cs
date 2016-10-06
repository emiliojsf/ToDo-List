using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FirstAPI.Models;
using FirstAPI.DTO;

namespace FirstAPI.Controllers
{
    public class ItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Items
        public IQueryable<ItemsDTO> GetItems()
        {
            //return db.Items;
            var items = from b in db.Items
                        select new ItemsDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Description = b.Description,
                            Date = b.Date,
                            Status = b.Status,
                            Hours = b.Hours,
                            Minute = b.Minute
                        };

            items = items.OrderBy(x => x.Minute).ThenBy(x => x.Hours).ThenBy(x=> x.Date);
            return items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(ItemsDTO))]
        public async Task<IHttpActionResult> GetItem(int id)
        {
            var item = await db.Items.Select(b =>
                new ItemsDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Date = b.Date,
                    Status = b.Status,
                    Hours = b.Hours,
                    Minute = b.Minute
                }).SingleOrDefaultAsync(b => b.Id == id);
            
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItem(int id, Items item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Items))]
        public async Task<IHttpActionResult> PostItem(Items item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            await db.SaveChangesAsync();

            var dto = new ItemsDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Date = item.Date,
                Status = item.Status,
                Hours = item.Hours,
                Minute = item.Minute
            };

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, dto);

        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Items))]
        public async Task<IHttpActionResult> DeleteItem(int id)
        {
            Items item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}