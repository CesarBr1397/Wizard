using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<ProductoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar caché distribuida y sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Cumple con GDPR si es necesario
});

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware para manejar redirecciones HTTPS
app.UseHttpsRedirection();

// Middleware para servir archivos estáticos
app.UseStaticFiles();

// Middleware de routing
app.UseRouting();

// Middleware de sesiones (agregarlo antes de los controladores)
app.UseSession();

// Middleware de autorización (opcional si tienes autenticación configurada)
app.UseAuthorization();

// Configurar rutas para controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

app.Run();
