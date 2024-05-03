using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Service;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Identity;
using MyShopApp.DAL.Interfaces;
using MyShopApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


string? connectionAppDb = builder.Configuration.GetConnectionString("ConnectionAppDb");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionAppDb));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<ApplicationUserManager>()
    .AddRoleManager<ApplicationRoleManager>();
//.AddSignInManager<ApplicationSignInManager>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWorks>();
builder.Services.AddScoped<IClientManager, ClientManager>();

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
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
