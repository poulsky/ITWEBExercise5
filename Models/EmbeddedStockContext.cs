using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace ITWEBExercise5.Models
{
    public class EmbeddedStockContext : DbContext
    {
        public EmbeddedStockContext(DbContextOptions<EmbeddedStockContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ESImage> EsImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable(nameof(Category));
            modelBuilder.Entity<Component>().ToTable(nameof(Component));
            modelBuilder.Entity<ComponentType>().ToTable(nameof(ComponentType));
            modelBuilder.Entity<ESImage>().ToTable(nameof(ESImage));

            modelBuilder.Entity<Category>().HasMany(e => e.ComponentTypes);
            modelBuilder.Entity<ComponentType>().HasMany(e => e.Categories);
            modelBuilder.Entity<ComponentType>().HasMany(e => e.Components);
        }
    }
}