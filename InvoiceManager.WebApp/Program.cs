using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;
using InvoiceManager.WebApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Sesja
builder.Services.AddDistributedMemoryCache(); // Użyj pamięci podręcznej, np. InMemoryCache
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas wygaśnięcia sesji
	options.Cookie.HttpOnly = true; // Dostęp tylko przez HTTP
	options.Cookie.IsEssential = true; // Wymagane dla zgodności z RODO
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
// Sesja - dodaj middleware sesji przed middleware obsługującym routing:
app.UseSession();

// Rotativa - do PDF
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");
app.UseRotativa();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
