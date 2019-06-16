using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalMerchBuild.DAL;
using FinalMerchBuild.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace FinalMerchBuild.Controllers
{
    public class SectionController : Controller
    {
        private MerchBuildContext db = new MerchBuildContext();

        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var sections = from s in db.Sections
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sections = sections.Where(s => s.SectionName.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    sections = sections.OrderByDescending(s => s.SectionName);
                    break;
                case "Date":
                    sections = sections.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    sections = sections.OrderByDescending(s => s.StartDate);
                    break;

            }
            return View(sections.ToList()); 
        }

        //// GET: Section
        //public ActionResult Index()
        //{
        //    return View(db.Sections.ToList());
        //}

        // GET: Section/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Section/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,SectionName,Height,Width,Depth,NumBays,StartDate")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
            }
            //  Create default Bays at the same time, these can be edited, if required
            {
                int bn = 1;
                int bw = 0;
                int nb = section.NumBays;
                Bay tmpBay = new Bay();
                Fixture tmpFix = new Fixture();


                for (int i = 0; i < nb; i++)
                {
                    tmpBay.SectionID = section.SectionID;
                    tmpBay.BayName = bn;
                    bn++;
                    tmpBay.BayHeight = section.Height;
                    tmpBay.BayDepth = section.Depth;
                    tmpBay.XLocation = bw;
                    tmpBay.BayWidth = section.Width / section.NumBays;
                    bw = bw + (Convert.ToInt32(tmpBay.BayWidth));
                    tmpBay.YLocation = 0;
                    if (section.Height > 100)
                    {
                        tmpBay.NumFix = (Convert.ToInt32(section.Height) / 25);
                    }
                    else
                    {
                        tmpBay.NumFix = (Convert.ToInt32(section.Height) / 20);
                    }

                    db.Bays.Add(tmpBay);

                    //  Create default Fixtures at the same time, these can be edited, if required
                    int fn = 1;
                    double xl = 0;
                    double yl = 0;
                    int nf = tmpBay.NumFix;

                    for (int f = 0; f < nf; f++)
                    {
                        tmpFix.SectionName = section.SectionName;
                        tmpFix.BayID = tmpBay.BayID;
                        tmpFix.BayName = tmpBay.BayName;
                        tmpFix.FixName = fn;
                        fn++;
                        tmpFix.FixHeight = 2;
                        tmpFix.FixWidth = tmpBay.BayWidth;
                        tmpFix.FixDepth = tmpBay.BayDepth;
                        xl = tmpBay.XLocation;
                        tmpFix.XLocation = xl;
                        tmpFix.YLocation = yl;
                        if (section.Height > 100)
                        {
                            yl = yl + 25;
                        }
                        else
                        {
                            yl = yl + 20;
                        }

                        db.Fixtures.Add(tmpFix);
                        db.SaveChanges();
                    }
                }
            }
            return View(section);
        }

        // GET: Section/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,SectionName,Height,Width,Depth,StartDate")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: Section/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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
