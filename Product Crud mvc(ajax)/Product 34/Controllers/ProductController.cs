using Product_34.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using PagedList;
using System.Data.Entity;
using Product_34.Models.vm;
using System.IO;
namespace Product_34.Controllers
{
    public class ProductController : Controller
    {
        //Install-Package PagedList.Mvc
        ProductDbContext db = new ProductDbContext();
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            //var products = db.Products.Include(p => p.Details.Select(d => d.Color)).OrderByDescending(p => p.PId).ToPagedList(pageNumber, pageSize);
            //db.Configuration.LazyLoadingEnabled = true;
            //var products = db.Products.OrderByDescending(p => p.PId).ToPagedList(pageNumber, pageSize);
            var products = db.Products.OrderByDescending(p => p.PId).ToPagedList(pageNumber, pageSize);
            foreach (var product in products)
            {
                db.Entry(product).Collection(p => p.Details).Load();
                foreach (var detail in product.Details)
                {
                    db.Entry(detail).Reference(d => d.Color).Load();
                }
            }

            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        public ActionResult Create(ProductVm productVm, int[] CId)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    PName = productVm.PName,
                    IsAviable = productVm.IsAviable,
                    Pdate = productVm.Pdate,
                    Price = productVm.Price,
                };
                HttpPostedFileBase file  = productVm.ImageFile;
                if (file != null) {
                    string filename = Path.Combine("/Images/", DateTime.Now.Ticks.ToString()+Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filename));
                    product.Image = filename;
                }
                foreach (var i in CId)
                {
                    var d = new Details()
                    {
                        Product = product,
                        PId = product.PId,
                        CId = i
                    };
                    db.Details.Add(d);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productVm);
        }
        public ActionResult AddColor(int? id)
        {
            ViewBag.Color = new SelectList(db.Colors.ToList(),"CId", "CName",id??0);
            return PartialView("AddColor");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var product = db.Products.Find(id);
            var details = db.Details.Where(e=>e.PId == product.PId).ToList();
            var pobj = new ProductVm()
            {
                PId = product.PId,
                Details = details,
                Image = product.Image,
                IsAviable = product.IsAviable,
                Pdate = product.Pdate,
                PName = product.PName,
                Price = product.Price,
            };
            return PartialView("Edit", pobj);
        }
        [HttpPost]
        public ActionResult Edit(ProductVm productVm, int[] CId)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(productVm.PId);
                var details = db.Details.Where(e => e.PId == product.PId).ToList();
                    product.PName = productVm.PName;
                    product.IsAviable = productVm.IsAviable;
                    product.Pdate = productVm.Pdate;
                    product.Price = productVm.Price;
              
                HttpPostedFileBase file = productVm.ImageFile;
                if (file != null)
                {
                    string filename = Path.Combine("/Images/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filename));
                    product.Image = filename;
                }
                else
                {
                    product.Image = product.Image;
                }
                db.Details.RemoveRange(details);
                foreach (var i in CId)
                {
                    var d = new Details()
                    {
                        Product = product,
                        PId = product.PId,
                        CId = i
                    };
                    db.Details.Add(d);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productVm);
        }
        public ActionResult Delete(int? id)
        {
            var product = db.Products.Find(id);
            var details = db.Details.Where(e => e.PId == product.PId).ToList();
            db.Details.RemoveRange(details);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}