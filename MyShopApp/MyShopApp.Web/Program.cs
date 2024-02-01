using Microsoft.EntityFrameworkCore;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Service;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.Interfaces;
using MyShopApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWorks>();

builder.Services.AddRazorPages();

builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapRazorPages();

app.Run();
