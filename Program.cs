using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManagementSchool.Data;
using ManagementSchool.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ManagementSchoolContextConnection") ?? throw new InvalidOperationException("Connection string 'ManagementSchoolContextConnection' not found.");

builder.Services.AddDbContext<ManagementSchoolContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplictionUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ManagementSchoolContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
