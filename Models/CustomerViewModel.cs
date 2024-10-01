namespace EcommerceApp.Models
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Constructor to initialize the properties
        public CustomerViewModel()
        {
            FirstName = string.Empty;  // Initialize to empty string
            LastName = string.Empty;
            Email = string.Empty;
        }
    }
}
