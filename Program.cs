namespace EcommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // Register HttpClient for making HTTP requests to external APIs (like Azure Functions)
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
                app.UseHsts();
            }

            app.UseHttpsRedirection();    // Redirect HTTP requests to HTTPS
            app.UseStaticFiles();         // Serve static files (like CSS, JS, images)

            app.UseRouting();             // Enable routing

            app.UseAuthorization();       // Enable authorization, if applicable

            // Define the default route, which points to HomeController and Index action.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the app
            app.Run();
        }
    }
}
