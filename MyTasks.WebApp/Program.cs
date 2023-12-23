using Microsoft.EntityFrameworkCore;
using MyTasks.WebApp.Core;
using MyTasks.WebApp.Core.Models.Domains;
using MyTasks.WebApp.Core.Service;
using MyTasks.WebApp.Persistence;
using MyTasks.WebApp.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// INFO - Dependency Injection
// w ka�dym miejscu w aplikacji gdzie u�ywane jest ITaskService, podstaw implementacj� TaskService
// s� 3 mo�liwo�ci w jaki spos�b mo�na wstrzykn�� ten serwis: 
//   - AddScoped - w ka�dym reque�cie danego klienta b�dzie tworzony nowy obiekt
//   - AddSingleton - w ca�ej aplikacji b�dzie tylko 1 instancja tego obiektu
//   - AddTransient - ka�de u�ycie (tutaj TaskService) powoduje stworzenie nowej instancji
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Task}/{action=Tasks}/{id?}");
app.MapRazorPages();

app.Run();
