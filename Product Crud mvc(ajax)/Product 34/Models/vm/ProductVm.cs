using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Product_34.Models.vm
{
    public class ProductVm
    {
        public ProductVm()
        {
            this.Details = new List<Details>();
        }
        public int PId { get; set; }
        [Required, DisplayName("Product Name")]
        public String PName { get; set; }
        public int Price { get; set; }
        public bool IsAviable { get; set; }
        public DateTime Pdate { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public virtual List<Details> Details { get; set; }
    }
}