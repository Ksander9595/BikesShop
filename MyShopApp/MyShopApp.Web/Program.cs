using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Service;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

//string? connectionUsersDb = builder.Configuration.GetConnectionString("ConnectionUsersDb");
string? connectionAppDb = builder.Configuration.GetConnectionString("ConnectionAppDb");
//builder.Services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(connectionUsersDb));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionAppDb));
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IRoleService, RoleService>();
//builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWorks>();
builder.Services.AddScoped<IidentityUnitOfWork, IdentityUnitOfWork>();

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
