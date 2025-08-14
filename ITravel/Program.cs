using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using ITravel.Models;
using ITravel.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using ITravel.Configurations.LayerConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TourismDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("AppDbConnectionString"), new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddService();

builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("ShareResource", assemblyName.Name);
    };
});

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>()
        {
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU"),
            new CultureInfo("uz-Latn-UZ"),

        };

        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

var localOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization(localOptions.Value);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
