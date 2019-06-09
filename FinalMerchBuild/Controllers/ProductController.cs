using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalMerchBuild.DAL;
using FinalMerchBuild.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;


namespace FinalMerchBuild.Controllers
{
    public class ProductController : Controller
    {
        private MerchBuildContext db = new MerchBuildContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(
                                new DataColumn[7] {
                                new DataColumn("Id", typeof(string)),
                                new DataColumn("UPC", typeof(string)),
                                new DataColumn("Name", typeof(string)),
                                new DataColumn("Size",typeof(string)),
                                new DataColumn("Height",typeof(decimal)),
                                new DataColumn("Width",typeof(decimal)),
                                new DataColumn("Depth",typeof(decimal)),
                                });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }
                string conString = ConfigurationManager.ConnectionStrings["MerchBuildContext"].ConnectionString;

                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Product";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        //sqlBulkCopy.ColumnMappings.Add( "ProductId", "Id");
                        sqlBulkCopy.ColumnMappings.Add("UPC", "UPC");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Size", "Size");
                        sqlBulkCopy.ColumnMappings.Add("Height", "Height");
                        sqlBulkCopy.ColumnMappings.Add("Width", "Width");
                        sqlBulkCopy.ColumnMappings.Add("Depth", "Depth");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,UPC,Name,Size,Height,Width,Depth")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,UPC,Name,Size,Height,Width,Depth")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //// POST: Product/Delete/5
        //[HttpPost, ActionName("DeleteAll")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteAllConfirmed(IEnumerable<int> productsToDelete)
        //{
        //    Product product = db.Products.Find(productsToDelete);
        //    db.Products.Where(x => productsToDelete.Contains(x.ProductId)).ToList().RemoveAll(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        ////// POST: Product/DeleteSel
        //[HttpPost, ActionName("DeleteSel")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteSel(IEnumerable<int> productsToDelete)
        //{
        //    db.Products.Where(x => productsToDelete.Contains(x.ProductId)).ForEach(db.Products.DeleteObject);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
