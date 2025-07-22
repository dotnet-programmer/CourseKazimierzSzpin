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
// s� 3 mo�liwo�ci w jaki spos�b mo�na wstrzykn�� ten serwis:
//   - AddScoped - w ka�dym jednych reque�cie danego klienta b�dzie u�ywana 1 instancja, czyli b�dzie tworzony jeden nowy obiekt tej klasy
//   - AddSingleton - w ca�ej aplikacji b�dzie tylko 1 instancja tego obiektu, kt�ra b�dzie u�ywana za ka�dym razem
//   - AddTransient - ka�de u�ycie (tutaj TaskService) powoduje stworzenie nowej instancji
// w ka�dym miejscu w aplikacji gdzie u�ywane jest ITaskService, podstaw implementacj� TaskService
builder.Services.AddScoped<ITaskService, TaskService>();
// w ka�dym miejscu w aplikacji gdzie u�ywane jest IApplicationDbContext, podstaw implementacj� ApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
// w ka�dym miejscu w aplikacji gdzie u�ywane jest IUnitOfWork, podstaw implementacj� UnitOfWork
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

// INFO - mo�liwo�� skonfigurowania potoku ��da� HTTP, na pocz�tku skonfigurowana jest reakcja na wyj�tki w aplikacji
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

// INFO - udost�pnianie plik�w statycznych zawartych w wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// INFO - konfiguracja domy�lnego rutingu
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Task}/{action=Tasks}/{id?}");
app.MapRazorPages();

app.Run();