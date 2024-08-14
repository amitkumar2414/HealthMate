using HealthMate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HealthMate;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    // Route configuration for Doctor area
    endpoints.MapAreaControllerRoute(
        name: "DoctorArea",
        areaName: "Doctor",
        pattern: "Doctor/{controller=Doctor}/{action=Index}/{id?}"
    );

    // Route configuration for Lab area
    endpoints.MapAreaControllerRoute(
        name: "LabotratoristArea",
        areaName: "Labotratorist",
        pattern: "Labotratorist/{controller=Lab}/{action=Index}/{id?}"
    );

    // Route configuration for Patient area (default)
    endpoints.MapAreaControllerRoute(
        name: "PatientArea",
        areaName: "Patient",
        pattern: "Patient/{controller=Home}/{action=Index}/{id?}"
    );

    // Fallback default route (in case no area is specified)
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=Patient}/{controller=Home}/{action=Index}/{id?}"
    );
});



//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "DoctorArea",
//        pattern: "{area:exists}/{controller=Doctor}/{action=Index}/{id?}"
//        );

//    endpoints.MapControllerRoute(
//        name: "Labotratorist",
//        pattern: "{area:exists}/{controller=Lab}/{action=Index}/{id?}"
//        );

//    endpoints.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Patient}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

//app.MapControllerRoute(
//    name: "forArea",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
