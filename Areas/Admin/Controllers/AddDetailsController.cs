using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using selfCoding.Areas.Admin.Models;
using selfCoding.Models;

namespace selfCoding.Areas.Admin.Controllers
{
    public class AddDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/AddDetails
        public ActionResult Index()
        {
            return View(db.AddDetails.ToList());
        }

        // GET: Admin/AddDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDetails addDetails = db.AddDetails.Find(id);
            if (addDetails == null)
            {
                return HttpNotFound();
            }
            return View(addDetails);
        }

        // GET: Admin/AddDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AddDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] AddDetails addDetails)
        {
            if (ModelState.IsValid)
            {
                db.AddDetails.Add(addDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addDetails);
        }

        // GET: Admin/AddDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDetails addDetails = db.AddDetails.Find(id);
            if (addDetails == null)
            {
                return HttpNotFound();
            }
            return View(addDetails);
        }

        // POST: Admin/AddDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] AddDetails addDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addDetails);
        }

        // GET: Admin/AddDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddDetails addDetails = db.AddDetails.Find(id);
            if (addDetails == null)
            {
                return HttpNotFound();
            }
            return View(addDetails);
        }

        // POST: Admin/AddDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddDetails addDetails = db.AddDetails.Find(id);
            db.AddDetails.Remove(addDetails);
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
