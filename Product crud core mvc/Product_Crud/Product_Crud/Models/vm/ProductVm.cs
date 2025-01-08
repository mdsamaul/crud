namespace Product_Crud.Models.vm
{
    public class ProductVm
    {
        public int PId { get; set; }
        public string PName { get; set; } = string.Empty;
        public int Price { get; set; }
        public bool IsAviable { get; set; }
        public DateOnly PDate { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
