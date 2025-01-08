using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Crud.Data;
using Product_Crud.Models;
using Product_Crud.Models.vm;

namespace Product_Crud.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _db;
        private readonly IWebHostEnvironment _ev;

        public ProductController(ProductDbContext db , IWebHostEnvironment ev)
        {
           _db = db;
            _ev = ev;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var product = await _db.products.Include(d=>d.Details)!.ThenInclude(c=>c.Colors).OrderByDescending(p=>p.PId).ToListAsync();
            return View(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProcut(ProductVm productVm, int[] CId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Procut Unable");
            }

            Product _product = new Product()
            {
                PName = productVm.PName,
                IsAviable = productVm.IsAviable,
                PDate = productVm.PDate,
                Price = productVm.Price,
            };

            if (productVm.ImageFile != null)
            {
                var file = DateTime.Now.Ticks.ToString() + Path.GetExtension(productVm.ImageFile.FileName);
                var fileName = _ev.WebRootPath + "/Images/" + file;
                using (var strem = System.IO.File.Create(fileName))
                {
                    productVm.ImageFile.CopyTo(strem);
                }
                _product.Image = "/Images/" + fileName;
            }

            foreach (var c in CId)
            {
                Details details = new Details()
                {
                    Products = _product,
                    PId = productVm.PId,
                    CId = c
                };
            }
            await _db.AddAsync(_product);
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");
        }
    }
}
