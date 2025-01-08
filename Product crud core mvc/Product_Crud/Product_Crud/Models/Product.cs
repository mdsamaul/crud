using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Crud.Models
{
   public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PId { get; set; }
        public string PName { get; set; }
        public int Price { get; set; }
        public bool IsAviable { get; set; }
        public DateOnly Pdate { get; set; }
        public string? Image { get; set; }
        public virtual IList<Details> Details { get; set; }
    }
}
