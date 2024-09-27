using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
  public DbSet<Product> Products { get; set; } 
  protected override void OnModelCreating(ModelBuilder modelBuilder){
    //un metodo
    //modelBuilder.Entity<Product>().Property(x=>x.Price).HasColumnType("decimal(18,2)");

    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurator).Assembly);

    
  }
}
