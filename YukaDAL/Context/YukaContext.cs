using Microsoft.EntityFrameworkCore;
using YukaDAL.Entities;

namespace YukaDAL.Context
{
    public class YukaContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SizeCategory> SizeCategories { get; set; }

        public YukaContext(DbContextOptions<YukaContext> options) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Brand configurations
            //Indicate the primary key
            modelBuilder.Entity<Brand>()
                .HasKey(b => b.BrandId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<Brand>()
                .Property(b => b.BrandId)
                .ValueGeneratedOnAdd();
            #endregion


            #region Color configurations
            //Indicate the primary key
            modelBuilder.Entity<Color>()
                .HasKey(c => c.ColorId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<Color>()
                .Property(c => c.ColorId)
                .ValueGeneratedOnAdd();
            #endregion


            #region Product configurations

            //Indicate the primary key
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            #endregion


            #region ProductVariant configurations
            //Indicate primary key
            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => p.VariantId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<ProductVariant>()
                .Property(p => p.VariantId)
                .ValueGeneratedOnAdd();

            //Relation N:1 with Product
            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Product)
                .WithMany(pr => pr.ProductVariants)
                .HasForeignKey(p => p.ProductId);

            //Relation N:1 with Size
            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Size)
                .WithMany(s => s.ProductVariants)
                .HasForeignKey(p => p.SizeId);

            //Relation N:1 with Color
            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Color)
                .WithMany(c => c.ProductVariants)
                .HasForeignKey(p => p.ColorId);

            //Relation N:1 with ProductBrandPriceGroup
            modelBuilder.Entity<ProductVariant>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.ProductVariants)
                .HasForeignKey(p => p.BrandId);

            #endregion


            #region Size configurations
            //Indicate the primary key
            modelBuilder.Entity<Size>()
                .HasKey(s => s.SizeId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<Size>()
                .Property(s => s.SizeId)
                .ValueGeneratedOnAdd();
            #endregion

            #region Category configurations
            modelBuilder.Entity<Category>()
                .HasKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .Property(p => p.CategoryId)
                .ValueGeneratedOnAdd();
            #endregion

            #region SizeCategory configurations
            modelBuilder.Entity<SizeCategory>()
                .HasKey(sc => new { sc.CategoryId, sc.SizeId }); // Clave compuesta

            modelBuilder.Entity<SizeCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(s => s.SizeCategories)
                .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<SizeCategory>()
                .HasOne(sc => sc.Size)
                .WithMany(c => c.SizeCategories)
                .HasForeignKey(sc => sc.SizeId);
            #endregion


        }
    }
}
