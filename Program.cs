using AirBB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


// EF Core context (update this connection name if you renamed it)
builder.Services.AddDbContext<AirBBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AirBBContext")));

// Enable session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Build app
var app = builder.Build();

// 2️⃣ Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

// 3️⃣ Routes — Admin first, then default
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 4️⃣ Run the app
app.Run();
