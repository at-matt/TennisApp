using Microsoft.EntityFrameworkCore;
using TennisCoach.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext with Entity Framework using SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout period
    options.Cookie.HttpOnly = true;  // Prevent client-side access to the cookie
    options.Cookie.IsEssential = true;  // Make the session cookie essential for GDPR compliance
});

// Add services for controllers and views
builder.Services.AddControllersWithViews();

// Add cookie-based authentication without Identity framework
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.Cookie.Name = "UserLoginCookie";  // Name of the authentication cookie
        options.LoginPath = "/Member/Login";  // Redirect to this path if not authenticated
        options.AccessDeniedPath = "/Home/AccessDenied";  // Redirect to this path if access is denied
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // Cookie expiration time
        options.SlidingExpiration = true;  // Extends cookie expiration time on each request
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Enforce HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();  // Serve static files

app.UseRouting();  // Enable request routing

// Add session and authentication middleware
app.UseSession();  // Enable session management
app.UseAuthentication();  // Enable cookie-based authentication
app.UseAuthorization();  // Enable authorization for authenticated users

// Define the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
