using chosen;
using chosen.Data;
using chosen.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

// 
builder.Services.AddDistributedMemoryCache(); // 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var yoyoFinalProjectConnectionString = builder.Configuration.GetConnectionString("yoyoFinalProject");
builder.Services.AddDbContext<FinalProjectContext>(options =>
    options.UseSqlServer(yoyoFinalProjectConnectionString));

var FinalProjectConnectionString = builder.Configuration.GetConnectionString("FinalProject");
builder.Services.AddDbContext<FinalProjectContext>(options =>
    options.UseSqlServer(FinalProjectConnectionString));

var yoyodbConnectionString = builder.Configuration.GetConnectionString("yoyodb");
builder.Services.AddDbContext<yoyodbContext>(options =>
    options.UseSqlServer(yoyodbConnectionString));

//var AzureConnectionString = builder.Configuration.GetConnectionString("Azure");
//builder.Services.AddDbContext<yoyodbContext>(options =>
//    options.UseSqlServer(AzureConnectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

// 
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

ConfigureServices(builder.Services, builder.Configuration); // 

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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Modified ConfigureServices method
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<yoyodbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("Azure"));
    });

    services.AddControllersWithViews();
    services.Configure<EmailSettings>(configuration.GetSection("EmailSettings_Google"));

    //services.AddAuthentication(options =>
    //{
    //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    //})
    //.AddCookie()
    //.AddGoogle(options =>
    //{
    //    options.ClientId = "814940950099-dcsgcs0vghj6gfeuf0jofonvrv0prmot.apps.googleusercontent.com";
    //    options.ClientSecret = "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";

    //});

    builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "814940950099-dcsgcs0vghj6gfeuf0jofonvrv0prmot.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    });



    services.AddDistributedMemoryCache(); // 
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // 
    });
}
