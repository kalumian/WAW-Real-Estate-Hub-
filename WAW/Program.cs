using WAW.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ≈÷«›… Œœ„«  MVC
builder.Services.AddControllersWithViews();

// ≈÷«›… DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ≈÷«›… HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// ≈÷«›… «·Ã·”… (Session)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // „œ… «·Ã·”…
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// «” Œœ«„ «·Ã·”…
app.UseSession();

// ÷»ÿ „”«— «· Õﬂ„
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
