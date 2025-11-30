using AirBB.Models.DataLayer.Repositories;
using AirBB.Models.DataLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Register MVC + Razor Views
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// 2️⃣ Register EF Core
builder.Services.AddDbContext<AirBBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AirBBContext")));

// 2️⃣ Register Repositories

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<ResidenceRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ExperienceRepository>();

// 3️⃣ Register Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 4️⃣ Configure the HTTP request pipeline
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

// Session MUST go before Authorization
app.UseSession();

app.UseAuthorization();

// 5️⃣ Routing (Admin first, then default)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
