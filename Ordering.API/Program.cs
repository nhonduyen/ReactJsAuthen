using Ordering.API.Extensions;
using Ordering.Infrastructure.Config;
using Ordering.Infrastructure;
using Ordering.Application.Commands.Customers.Create;
using Ordering.Application.Commands.User.Create;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAuthen(jwtSettings);

// Include Infrastructur Dependency
builder.Services.AddInfrastructure(builder.Configuration);

// Register dependencies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateCustomerCommandHandler).GetTypeInfo().Assembly));

builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// maintain middleware order
app.UseCors("CorsPolicy");

// Added for authentication
// Maintain middleware order
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
