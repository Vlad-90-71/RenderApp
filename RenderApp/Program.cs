using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RenderApp;
using RenderApp.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RenderAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Применяем миграции автоматически при старте (удобно для Render)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RenderAppDbContext>();
    db.Database.Migrate();
}

// Минимальные эндпоинты для проверки
app.MapGet("/users", async (RenderAppDbContext db) => await db.Users.ToListAsync()); 
app.MapPost("/users", async (RenderAppDbContext db, User u) => 
{ 
    db.Users.Add(u); 
    await db.SaveChangesAsync(); 
    return Results.Created($"/users/{u.Id}", u); 
});

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
