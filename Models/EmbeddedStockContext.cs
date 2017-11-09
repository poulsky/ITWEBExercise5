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
        public DbSet<CategoryComponentType> CategoryComponentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable(nameof(Category));
            modelBuilder.Entity<Component>().ToTable(nameof(Component));
            modelBuilder.Entity<ComponentType>().ToTable(nameof(ComponentType));
            modelBuilder.Entity<ESImage>().ToTable(nameof(ESImage));

            modelBuilder.Entity<ComponentType>().HasMany(e => e.Components);

            modelBuilder.Entity<CategoryComponentType>()
                .HasKey(cc => new {cc.CategoryId, cc.ComponentTypeId});

            modelBuilder.Entity<CategoryComponentType>()
                .HasOne(cc => cc.ComponentType)
                .WithMany(co => co.CategoryComponentTypes)
                .HasForeignKey(cc => cc.ComponentTypeId);

            modelBuilder.Entity<CategoryComponentType>()
                .HasOne(cc => cc.Category)
                .WithMany(co => co.CategoryComponentTypes)
                .HasForeignKey(cc => cc.CategoryId);
        }
    }
}