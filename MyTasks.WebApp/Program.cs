using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Services;
using MyTasks.WebApp.Persistence;
using MyTasks.WebApp.Persistence.Services;

// ASP.NET Core Web Application - MVC + Authentication - Individual User Accounts (Store user accounts in-app)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// INFO - Dependency Injection
// są 3 możliwości w jaki sposób można wstrzyknąć ten serwis:
//   - AddScoped - w każdym jednych requeście danego klienta będzie używana 1 instancja, czyli będzie tworzony jeden nowy obiekt tej klasy
//   - AddSingleton - w całej aplikacji będzie tylko 1 instancja tego obiektu, która będzie używana za każdym razem
//   - AddTransient - każde użycie (tutaj TaskService) powoduje stworzenie nowej instancji
// w każdym miejscu w aplikacji gdzie używane jest ITaskService, podstaw implementację TaskService
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// INFO - możliwość skonfigurowania potoku żądań HTTP, na początku skonfigurowana jest reakcja na wyjątki w aplikacji
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

app.UseHttpsRedirection();

// INFO - udostępnianie plików statycznych zawartych w wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// INFO - konfiguracja domyślnego rutingu
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Task}/{action=Tasks}/{id?}");
app.MapRazorPages();

app.Run();