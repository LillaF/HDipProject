using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalMerchBuild.DAL;
using FinalMerchBuild.Models;

namespace FinalMerchBuild
{
    public class BayController : Controller
    {
        private MerchBuildContext db = new MerchBuildContext();

        // GET: Bay
        public ActionResult Index()
        {
            var bays = db.Bays.Include(b => b.Section);
            return View(bays.ToList());
        }

        // GET: Bay/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bay bay = db.Bays.Find(id);
            if (bay == null)
            {
                return HttpNotFound();
            }
            return View(bay);
        }

        // GET: Bay/Create
        // GET: Bay/Create
        public ActionResult Create(Section section)
        {
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName");
            Bay newBay = db.Bays.OrderByDescending(b => b.BayID).FirstOrDefault();
            //var bays = db.Bays.Include(b => b.SectionName);

            newBay.XLocation = newBay.XLocation + newBay.BayWidth;
            newBay.BayWidth = 0;
            newBay.BayName++;
            return View(newBay);
        }

        // POST: Bay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BayID,SectionID,SectionName,BayName,BayWidth,XLocation,YLocation")] Bay bay)
        {
            if (ModelState.IsValid)
            {
                db.Bays.Add(bay);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", bay.SectionID);
            return View(bay);
        }

        // GET: Bay/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bay bay = db.Bays.Find(id);
            if (bay == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", bay.SectionID);
            return View(bay);
        }

        // POST: Bay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BayID,SectionID,SectionName,BayName,BayWidth,XLocation,YLocation")] Bay bay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", bay.SectionID);
            return View(bay);
        }

        // GET: Bay/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bay bay = db.Bays.Find(id);
            if (bay == null)
            {
                return HttpNotFound();
            }
            return View(bay);
        }

        // POST: Bay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bay bay = db.Bays.Find(id);
            db.Bays.Remove(bay);
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
