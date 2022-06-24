using Data;
using Data.Repositories;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Abstract;
using static Services.CompaniesService;
using static Services.CVsService;
using static Services.ProjectPhotoService;
using static Services.ProjectsService;
using static Services.TechnologiesService;
using static Services.UniversitiesService;
using static Services.UsersService;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllersWithViews();

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
     b => b.MigrationsAssembly("Data")
     ));

// добавить все сервисы в сервис коллекцию
builder.Services.AddTransient<ApplicationContext, ApplicationContext>();

builder.Services.AddTransient<ICompaniesService, CompaniesService>();
builder.Services.AddTransient<ICompaniesRepository, CompaniesRepository>();

builder.Services.AddTransient<IUniversitiesService, UniversitiesService>();
builder.Services.AddTransient<IUniversitiesRepository, UniversitiesRepository>();

builder.Services.AddTransient<ITechnologiesService, TechnologiesService>();
builder.Services.AddTransient<ITechnologiesRepository, TechnologiesRepository>();

builder.Services.AddTransient<IProjectsService, ProjectsService>();
builder.Services.AddTransient<IProjectsRepository, ProjectsRepository>();

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

builder.Services.AddTransient<ICVsService, CVsService>();
builder.Services.AddTransient<ICVsRepository, CVsRepository>();

builder.Services.AddTransient<IProfilePhotoService, ProfilePhotoService>();
builder.Services.AddTransient<IProjectPhotoService, ProjectPhotoService>();
builder.Services.AddTransient<IProjectPhotoRepository, ProjectPhotoRepository>();
builder.Services.AddTransient<IPdfService, PdfService>();

builder.Services.AddAutoMapper(typeof(AppMappingUser), typeof(AppMappingCompany), typeof(AppMappingTechnology), typeof(AppMappingUniversity), typeof(AppMappingProject), typeof(AppMappingCV), typeof(AppMappingProjectPhoto)); 

 builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.Run("http://localhost:3001");
