using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product_Crud.Models;
using Product_Crud.Models.vm;

namespace Product_Crud.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext db;
        private readonly IWebHostEnvironment en;

        public ProductController(ProductDbContext db, IWebHostEnvironment en)
        {
            this.db = db;
            this.en = en;
        }
        public IActionResult Index()
        {
            var product = db.Products.Include(e => e.Details).ThenInclude(g => g.Color).OrderByDescending(c => c.PId).ToList();
            return View(product);
        }
        public IActionResult Create()
        {
            ViewBag.Color = new SelectList(db.Colors.ToList(), "CId", "CName");
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(ProductVm productVm, int[] CId)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    PName = productVm.PName,
                    Pdate = productVm.Pdate,
                    IsAviable = productVm.IsAviable,
                    Price = productVm.Price
                };
                if (productVm.ImageFile != null)
                {
                    var file = DateTime.Now.Ticks.ToString() + Path.GetExtension(productVm.ImageFile.FileName);
                    var fileName = en.WebRootPath + "/Images/" + file;
                    using (var strem = System.IO.File.Create(fileName))
                    {
                        productVm.ImageFile.CopyTo(strem);
                    }
                    product.Image = "/Images/" + file;
                }
                foreach (var i in CId)
                {
                    Details d = new Details()
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
        public IActionResult AddColor(int? id)
        {
            ViewBag.Color = new SelectList(db.Colors.ToList(), "CId", "CName", id ?? 0);
            return PartialView("_addColor");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Color = new SelectList(db.Colors.ToList(), "CId", "CName");
            var product = db.Products.Find(id);
            var details = db.Details.Where(c => c.PId == product.PId).ToList();
            var p = new ProductVm()
            {
                PId = product.PId,
                PName = product.PName,
                Details = details,
                Image = product.Image,
                IsAviable = product.IsAviable,
                Pdate = product.Pdate,
                Price = product.Price,
            };

            return View("Edit", p);
        }
        [HttpPost]
        public IActionResult Edit(ProductVm productVm, int[] CId)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(productVm.PId);
                var details = db.Details.Where(c => c.PId == product.PId).ToList();
                product.PName = productVm.PName;
                product.Pdate = productVm.Pdate;
                product.IsAviable = productVm.IsAviable;
                product.Price = productVm.Price;
                if (productVm.ImageFile != null)
                {
                    var file = DateTime.Now.Ticks.ToString() + Path.GetExtension(productVm.ImageFile.FileName);
                    var fileName = en.WebRootPath + "/Images/" + file;
                    using (var strem = System.IO.File.Create(fileName))
                    {
                        productVm.ImageFile.CopyTo(strem);
                    }
                    product.Image = "/Images/" + file;
                }
                else
                {
                    product.Image = product.Image;
                }
                db.Details.RemoveRange(details);
                foreach (var i in CId)
                {
                    Details d = new Details()
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
        public IActionResult Delete(int? id)
        {
            var product = db.Products.Find(id);
            var details = db.Details.Where(c => c.PId == product.PId).ToList();
            db.Details.RemoveRange(details);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
