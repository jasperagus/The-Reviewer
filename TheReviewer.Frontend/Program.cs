using TheReviewer.Data.Interfaces;
using TheReviewer.Data.Repositories;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not configured.");
}

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFilmRepository>(_ => new FilmRepository(connectionString));
builder.Services.AddScoped<IGameRepository>(_ => new GameRepository(connectionString));
builder.Services.AddScoped<IReviewerRepository>(_ => new ReviewerRepository(connectionString));
builder.Services.AddScoped<IReviewRepository>(_ => new ReviewRepository(connectionString));

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IReviewerService, ReviewerService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

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