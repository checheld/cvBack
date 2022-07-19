#region Imports
using CloudinaryDotNet;
using Data;
using Data.Repositories.Utility;
using Data.Repositories.Utility.Interface;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Abstract;
using Services.Utility;
using Services.Utility.Interface;
using static Services.CompaniesService;
using static Services.CVsService;
using static Services.ProfilePhotoService;
using static Services.ProjectPhotoService;
using static Services.ProjectsService;
using static Services.TechnologiesService;
using static Services.UniversitiesService;
using static Services.UsersService;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

IConfiguration configuration = builder.Configuration;

/*builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
     b => b.MigrationsAssembly("Data")
     ));*/

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"],
     b => b.MigrationsAssembly("Data")
     ));

builder.Services.AddTransient<ApplicationContext, ApplicationContext>();

builder.Services.AddTransient<IPdfService, PdfService>();
builder.Services.AddTransient<IServiceManager, ServiceManager>();
builder.Services.AddTransient<IRepositoryManager, RepositoryManager>();

builder.Services.AddAutoMapper(typeof(AppMappingUser), typeof(AppMappingCompany), 
    typeof(AppMappingTechnology), typeof(AppMappingUniversity), typeof(AppMappingProject), 
    typeof(AppMappingCV), typeof(AppMappingProjectPhoto), typeof(AppMappingPhotoParams));

builder.Services.AddRazorPages();

var cloudinarySettings = new Account
{
    Cloud = configuration.GetSection("CloudinarySettings")["CloudName"],
    ApiKey = configuration.GetSection("CloudinarySettings")["ApiKey"],
    ApiSecret = configuration.GetSection("CloudinarySettings")["ApiSecret"]
};
builder.Services.AddSingleton(cloudinarySettings);

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseCors(x => x
                .WithOrigins()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.MapRazorPages();

app.Run("http://localhost:3001");
/*app.Run();*/
