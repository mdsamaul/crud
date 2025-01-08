namespace Product_Crud.Models.vm
{
    public class ProductVm
    {
        public ProductVm()
        {
            this.Details = new List<Details>();
        }
        public int PId { get; set; }
        public string PName { get; set; }
        public int Price { get; set; }
        public bool IsAviable { get; set; }
        public DateOnly Pdate { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public virtual List<Details> Details { get; set; }
    }
}
