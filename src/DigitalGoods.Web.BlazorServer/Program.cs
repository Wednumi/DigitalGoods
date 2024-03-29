using DigitalGoods.Infrastructure.ServiceConfiguration;
using DigitalGoods.Core;
using System.Reflection;
using DigitalGoods.Web.BlazorServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddTransient<OfferIdParser>();

var coreAssembly = Assembly.Load("DigitalGoods.Core");
var currentAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(coreAssembly, currentAssembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();