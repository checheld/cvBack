using Data;
using Data.Repositories;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Abstract;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllersWithViews();

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
     b => b.MigrationsAssembly("LeviossaCV")
     ));

// добавить все сервисы в сервис коллекцию
builder.Services.AddTransient<ApplicationContext, ApplicationContext>();

builder.Services.AddTransient<ICompaniesService, CompaniesService>();
builder.Services.AddTransient<ICompaniesRepository, CompaniesRepository>();

builder.Services.AddTransient<IUniversitiesService, UniversitiesService>();
builder.Services.AddTransient<IUniversitiesRepository, UniversitiesRepository>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseCors(x => x
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

app.Run("http://localhost:3001");
