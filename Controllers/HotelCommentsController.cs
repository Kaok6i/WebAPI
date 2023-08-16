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
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    public class HotelCommentsController : ApiController
    {
        private TourBaseEntities db = new TourBaseEntities();

        // GET: api/HotelComments
        public IQueryable<HotelComment> GetHotelComment()
        {
            return db.HotelComment;
        }
        [Route("api/getHotelComments")]
        public IHttpActionResult GetHotelComments (int hotelid)
        {
            var hotelComments = db.HotelComment.ToList().Where(p=>p.HotelID == hotelid).ToList();
            return Ok(hotelComments);
        }
        // GET: api/HotelComments/5
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult GetHotelComment(int id)
        {
            HotelComment hotelComment = db.HotelComment.Find(id);
            if (hotelComment == null)
            {
                return NotFound();
            }

            return Ok(hotelComment);
        }

        // PUT: api/HotelComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHotelComment(int id, HotelComment hotelComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelComment.ID)
            {
                return BadRequest();
            }

            db.Entry(hotelComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelCommentExists(id))
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

        // POST: api/HotelComments
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult PostHotelComment(HotelComment hotelComment)
        {
            hotelComment.CreationDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(hotelComment.Author) || hotelComment.Author.Length > 100)
                ModelState.AddModelError("Author","Author is required string up to 100 symbols.");
            if (string.IsNullOrWhiteSpace(hotelComment.Text))
                ModelState.AddModelError("Text","Text is required string.");
            if (!(db.Hotel.ToList().FirstOrDefault(p => p.ID == hotelComment.HotelID) is Hotel))
                ModelState.AddModelError("HotelId","HotelId is hotel's id from database");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HotelComment.Add(hotelComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotelComment.ID }, hotelComment);
        }

        // DELETE: api/HotelComments/5
        [ResponseType(typeof(HotelComment))]
        public IHttpActionResult DeleteHotelComment(int id)
        {
            HotelComment hotelComment = db.HotelComment.Find(id);
            if (hotelComment == null)
            {
                return NotFound();
            }

            db.HotelComment.Remove(hotelComment);
            db.SaveChanges();

            return Ok(hotelComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelCommentExists(int id)
        {
            return db.HotelComment.Count(e => e.ID == id) > 0;
        }
    }
}