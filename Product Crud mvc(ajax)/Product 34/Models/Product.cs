using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_34.Models
{
    public class Product
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PId { get; set; }
        public String PName { get; set; }
        public int Price { get; set; }
        public bool IsAviable { get; set; }
        public DateTime Pdate { get; set; }
        public string Image { get; set; }
        public virtual IList<Details> Details { get; set; }
    }
}