using Microsoft.AspNetCore.Authentication.Cookies;
using TheReviewer.Data.Interfaces;
using TheReviewer.Data.Repositories;
using TheReviewer.Logic.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not configured.");
}

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddScoped<IReviewerRepository>(_ => new ReviewerRepository(connectionString));
builder.Services.AddScoped<IReviewRepository>(_ => new ReviewRepository(connectionString));
builder.Services.AddScoped<IMediaRepository>(_ => new MediaRepository(connectionString));
builder.Services.AddScoped<ReviewerService>();
builder.Services.AddScoped<MediaService>();
builder.Services.AddScoped<ReviewService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

