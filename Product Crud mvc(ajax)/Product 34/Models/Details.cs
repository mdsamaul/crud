using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_34.Models
{
    public class Details
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DId { get; set; }
        [ForeignKey("Product")]
        public int PId { get; set; }
        [ForeignKey("Color")]
        public int CId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
    }
}