using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog1.Data;
using Blog1.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));



builder.Services.AddIdentity<CustomUser, CustomRole>(
options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.MaxLengthForKeys = 128;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddRoles<CustomRole>()
.AddDefaultUI()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();


var app = builder.Build();
using (var scope=app.Services.CreateScope()){
    var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<CustomRole>>();
    var userManager=scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
    string[] roles={"Admin","Contributor"};
    foreach(var role in roles){
        if(!await roleManager.RoleExistsAsync(role)){
            await roleManager.CreateAsync(new CustomRole{Name=role});
        }
    }
}
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
