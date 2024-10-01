namespace EcommerceApp.Models
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }

        // Add a constructor to initialize the properties
        public ProductViewModel()
        {
            ProductName = string.Empty;  // Initialize with an empty string
            Description = string.Empty;
            ImageBase64 = string.Empty;
        }
    }
}
