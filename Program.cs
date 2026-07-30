using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework para PostgreSQL / Supabase
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// 1. Endpoint para Unidad 1: Ciclo de Vida y Entornos
app.MapGet("/api/status", () =>
{
    return Results.Text("El servidor está activado!");
});

// 2. Endpoint para Unidad 3 y 4: Persistencia y prueba de lectura
app.MapGet("/api/items", async (AppDbContext db) => await db.Items.ToListAsync());

// 3. Endpoint para probar escrituras / CORS desde clientes web
app.MapPost("/api/items", async (Item item, AppDbContext db) => {
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/items/{item.Id}", item);
});

app.Run();