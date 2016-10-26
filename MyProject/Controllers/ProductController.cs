using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CatName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase Imagefile)
        {
            
            if (ModelState.IsValid)
            {    
                    if (Imagefile != null)
                    {
                        string extension = System.IO.Path.GetExtension(Imagefile.FileName);
                        string filename = Path.GetFileName(Imagefile.FileName);
                        //TODO: change this to relative path and add it to the config
                        string path = Path.Combine(Server.MapPath("~/images"), filename);
                        Imagefile.SaveAs(path);
                        //product.ProdImageUrl = @"~/images" + product.ProductId + extension;
                        product.ProdImageUrl = @"~/images/" + filename;
                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CatName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CatName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProdName,ProdPrice,ProdImageUrl,ProdDescription,CategoryId")] Product product,HttpPostedFileBase Imagefile)
        {
            if (ModelState.IsValid)
            {
                if (Imagefile != null)
                {
                    string extension = System.IO.Path.GetExtension(Imagefile.FileName);
                    string filename = Path.GetFileName(Imagefile.FileName);
                    //TODO: change this to relative path and add it to the config
                    string path = Path.Combine(Server.MapPath("~/images"), filename);
                    Imagefile.SaveAs(path);
                    product.ProdImageUrl = @"~/images/" + filename;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CatName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
