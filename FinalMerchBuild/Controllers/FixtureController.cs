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

namespace FinalMerchBuild.Controllers
{
    public class FixtureController : Controller
    {
        private MerchBuildContext db = new MerchBuildContext();

        // GET: Fixture
        public ActionResult Index()
        {
            var fixtures = db.Fixtures.Include(f => f.Position).Include(f => f.Section);
            return View(fixtures.ToList());
        }

        // GET: Fixture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // GET: Fixture/Create
        public ActionResult Create()
        {
            ViewBag.PositionID = new SelectList(db.Bays, "PositionID", "UPC");
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName");
            return View();
        }

        // POST: Fixture/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FixtureID,SectionID,PositionID,ProductName")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                db.Fixtures.Add(fixture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Bays, "PositionID", "UPC", fixture.PositionID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", fixture.SectionID);
            return View(fixture);
        }

        // GET: Fixture/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.Bays, "PositionID", "UPC", fixture.PositionID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", fixture.SectionID);
            return View(fixture);
        }

        // POST: Fixture/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FixtureID,SectionID,PositionID,ProductName")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionID = new SelectList(db.Bays, "PositionID", "UPC", fixture.PositionID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "SectionName", fixture.SectionID);
            return View(fixture);
        }

        // GET: Fixture/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // POST: Fixture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fixture fixture = db.Fixtures.Find(id);
            db.Fixtures.Remove(fixture);
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
