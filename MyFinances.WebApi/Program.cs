using Microsoft.EntityFrameworkCore;
using MyFinances.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// INFO - do³¹czenie plików XML z komentarzami XML do Swaggera
// pakiet NuGet - Swashbuckle.AspNetCore -> póŸniej konfiguracja -> builder.Services.AddSwaggerGen();
// projekt -> w³aœciwoœci -> Build -> Output - zaznaczyæ XML documentation file
// po w³¹czeniu trzeba dodaæ do ka¿dej akcji komentarz XML, czyli ten: ///
// w konfiguracji wskazaæ plik xml:
//builder.Services.AddSwaggerGen(x =>
//{
//	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//	x.IncludeXmlComments(xmlPath);
//});
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MyFinancesContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	// Swagger
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();