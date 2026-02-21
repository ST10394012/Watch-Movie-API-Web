using Movie_API_Web.Services;

namespace Movie_API_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Controllers and Views
            builder.Services.AddControllersWithViews();

            // Register HttpClient for TMDb API calls
            builder.Services.AddHttpClient();

            // Register TmdbService with dependency injection
            // This allows controllers to receive TmdbService via constructor injection
            builder.Services.AddScoped<TmdbService>();

            // Register CommentService for JSON file operations
            builder.Services.AddScoped<CommentService>();

            // Load configuration from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Serve static files from wwwroot folder
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Configure default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}