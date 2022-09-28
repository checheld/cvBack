#region Imports
using CloudinaryDotNet;
using Data;
using Data.Repositories.Utility;
using Data.Repositories.Utility.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Abstract;
using Services.Utility;
using Services.Utility.Interface;
using System.Text.Json.Serialization;
using static API.Controllers.CompaniesController;
using static API.Controllers.UniversitiesController;
using static LeviossaCV.Controllers.ProjectsController;
using static LeviossaCV.Controllers.ProjectTypesController;
using static LeviossaCV.Controllers.TechnologiesController;
using static LeviossaCV.Controllers.UsersController;
using static Services.CompaniesService;
using static Services.CVsService;
using static Services.ProfilePhotoService;
using static Services.ProjectPhotoService;
using static Services.ProjectsService;
using static Services.Services.ProjectTypesService;
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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        /* opt.Authority = "https://localhost:5001";
         opt.Audience = "https://localhost:5001/resources";*/
        opt.Authority = "http://identity-server-1.herokuapp.com";
        opt.Audience = "http://identity-server-1.herokuapp.com/resources";
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuer = true,
            /*ValidIssuer = "https://localhost:5001"*/
            ValidIssuer = "http://identity-server-1.herokuapp.com"
        };
    });

builder.Services.AddAuthorization(options =>
{
    // Add a policy called "ApiScope" which is required our users are authenticated and have the API Scope Claim call WebAPI 
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope2");
    });
});

builder.Services.AddTransient<ApplicationContext, ApplicationContext>();

builder.Services.AddTransient<IPdfService, PdfService>();
builder.Services.AddTransient<IServiceManager, ServiceManager>();
builder.Services.AddTransient<IRepositoryManager, RepositoryManager>();

builder.Services.AddAutoMapper(typeof(AppMappingUser), typeof(AppMappingCompany),
    typeof(AppMappingTechnology), typeof(AppMappingUniversity), typeof(AppMappingProject),
    typeof(AppMappingCV), typeof(AppMappingProjectPhoto), typeof(AppMappingPhotoParams),
    typeof(AppMappingProjectType), typeof(AppMappingUniversityController), typeof(AppMappingProjectTypeController),
    typeof(AppMappingCompanyController), typeof(AppMappingTechnologyController), typeof(AppMappingProjectsController));

// this needed for ignore cyclic json objects
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Для запятых
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.AllowTrailingCommas = true);

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
app.UseAuthentication();
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

/*app.Run("http://localhost:3001");*/
app.Run();