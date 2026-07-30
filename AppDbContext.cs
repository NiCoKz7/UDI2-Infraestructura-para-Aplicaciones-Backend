using Microsoft.EntityFrameworkCore;

// Modelo simple de ejemplo para las pruebas
public class Item
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

// Contexto de Base de Datos
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Item> Items => Set<Item>();
}