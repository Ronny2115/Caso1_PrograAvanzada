using Caso1_PrograAvanzada.DAL;
using Caso1_PrograAvanzada.BLL;
using Caso1_PrograAvanzada.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar la clase de conexión para inyección de dependencias
builder.Services.AddSingleton<Conexion>();
builder.Services.AddScoped<HabitacionDAL>();
builder.Services.AddScoped<HabitacionBLL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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