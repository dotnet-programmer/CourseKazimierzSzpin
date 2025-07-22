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
// s¹ 3 mo¿liwoœci w jaki sposób mo¿na wstrzykn¹æ ten serwis:
//   - AddScoped - w ka¿dym jednych requeœcie danego klienta bêdzie u¿ywana 1 instancja, czyli bêdzie tworzony jeden nowy obiekt tej klasy
//   - AddSingleton - w ca³ej aplikacji bêdzie tylko 1 instancja tego obiektu, która bêdzie u¿ywana za ka¿dym razem
//   - AddTransient - ka¿de u¿ycie (tutaj TaskService) powoduje stworzenie nowej instancji
// w ka¿dym miejscu w aplikacji gdzie u¿ywane jest ITaskService, podstaw implementacjê TaskService
builder.Services.AddScoped<ITaskService, TaskService>();
// w ka¿dym miejscu w aplikacji gdzie u¿ywane jest IApplicationDbContext, podstaw implementacjê ApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
// w ka¿dym miejscu w aplikacji gdzie u¿ywane jest IUnitOfWork, podstaw implementacjê UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// INFO - mo¿liwoœæ skonfigurowania potoku ¿¹dañ HTTP, na pocz¹tku skonfigurowana jest reakcja na wyj¹tki w aplikacji
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

// INFO - udostêpnianie plików statycznych zawartych w wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// INFO - konfiguracja domyœlnego rutingu
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Task}/{action=Tasks}/{id?}");
app.MapRazorPages();

app.Run();