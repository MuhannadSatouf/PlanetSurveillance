using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlanetSurveillance.Data;
using PlanetSurveillance.Data.Repositories.PersonRepo;
using PlanetSurveillance.Data.Repositories.PlanetRepo;
using PlanetSurveillance.Data.Repositories.VisitRepo;
using PlanetSurveillance.Services.PlanetService;
using PlanetSurveillance.Services.VisitService;
using PlanetSurveillance.Services.PersonService;
using PlanetSurveillance.Services.Swapi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<PlanetSurveillanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPlanetService, PlanetService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddHttpClient<ISwapiService, SwapiService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Planet Surveillance API", Version = "v1" });

    c.EnableAnnotations();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planet Surveillance API V1");
        c.RoutePrefix = string.Empty;  
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
