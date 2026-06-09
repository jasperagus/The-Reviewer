using TheReviewer.Data.Interfaces;
using TheReviewer.Data.Repositories;
using TheReviewer.Logic.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not configured.");
}

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IReviewerRepository>(_ => new ReviewerRepository(connectionString));
builder.Services.AddScoped<IReviewRepository>(_ => new ReviewRepository(connectionString));

builder.Services.AddScoped<IMediaRepository>(_ => new MediaRepository(connectionString));
builder.Services.AddScoped<MediaService>();
builder.Services.AddScoped<ReviewerService>();
builder.Services.AddScoped<ReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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


app.Run();