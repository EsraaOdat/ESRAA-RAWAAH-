using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication29.Models;

namespace WebApplication29.Controllers
{
    public class contact_usController : Controller
    {
        private ELECTIONSEntities db = new ELECTIONSEntities();

        // GET: contact_us

        public ActionResult Index(bool? isRead)
        {
            var contacts = db.contact_us.AsQueryable();
            if (isRead.HasValue)
            {
                if (isRead.Value)
                {
                    contacts = contacts.Where(c => c.IsRead == true);
                }
                else
                {
                    contacts = contacts.Where(c => c.IsRead == false || c.IsRead == null);
                }
            }
            return View(contacts.ToList());
        }

        /*public ActionResult Index()
        {
            return View(db.contact_us.ToList());
        }


        // Index method with filter
        [HttpGet]
        public ActionResult FilteredIndex(bool? isRead)
        {
            var contacts = db.contact_us.AsQueryable();
            if (isRead.HasValue)
            {
                contacts = contacts.Where(c => c.IsRead == isRead.Value);
            }
            return View("Index", contacts.ToList());
        }*/


        // GET: contact_us/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact_us contact_us = db.contact_us.Find(id);
            if (contact_us == null)
            {
                return HttpNotFound();
            }
            // Set IsRead to true
            contact_us.IsRead = true;
            db.Entry(contact_us).State = EntityState.Modified;
            db.SaveChanges();

            return View(contact_us);
        }

        // GET: contact_us/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: contact_us/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,name,subject,message,IsRead")] contact_us contact_us)
        {
            if (ModelState.IsValid)
            {
                db.contact_us.Add(contact_us);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Your message has been sent. Thank you for contacting us!";
                return RedirectToAction("Create");
            }

            return View(contact_us);
        }

        /*
                // POST: contact_us/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create([Bind(Include = "id,email,name,subject,message,IsRead")] contact_us contact_us)
                {
                    if (ModelState.IsValid)
                    {
                        db.contact_us.Add(contact_us);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(contact_us);
                }
        */
        // GET: contact_us/Edit/5





        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact_us contact_us = db.contact_us.Find(id);
            if (contact_us == null)
            {
                return HttpNotFound();
            }
            return View(contact_us);
        }

        // POST: contact_us/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,name,subject,message,IsRead")] contact_us contact_us)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact_us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact_us);
        }

        // GET: contact_us/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact_us contact_us = db.contact_us.Find(id);
            if (contact_us == null)
            {
                return HttpNotFound();
            }

            // Set IsRead to true
            contact_us.IsRead = true;
            db.Entry(contact_us).State = EntityState.Modified;
            db.SaveChanges();

            return View(contact_us);

        }

        // POST: contact_us/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contact_us contact_us = db.contact_us.Find(id);
            db.contact_us.Remove(contact_us);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
