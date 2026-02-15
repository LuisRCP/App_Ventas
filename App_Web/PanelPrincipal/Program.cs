using Microsoft.EntityFrameworkCore;
using PanelPrincipal.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Agregar MVC + API
builder.Services.AddControllersWithViews();

// 🔹 Registrar DbContext
builder.Services.AddDbContext<TiendaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 🔹 Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   // ← IMPORTANTE
app.UseRouting();
app.UseAuthorization();

// 🔹 Habilita ApiController
app.MapControllers();

// 🔹 Ruta MVC tradicional
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();