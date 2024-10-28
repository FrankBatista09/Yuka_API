using Microsoft.EntityFrameworkCore;
using YukaDAL.Entities;

namespace YukaDAL.Context
{
    public class YukaContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrandPriceGroup> ProductBrandPriceGroups { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeGroup> SizeGroups { get; set; }

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
            #endregion


            #region ProductBrandPriceGroup configurations
            //Indicate the primary key
            modelBuilder.Entity<ProductBrandPriceGroup>()
                .HasKey(p => p.PriceGroupId);

            //Indicate the primary key is generated on add(identity)
            modelBuilder.Entity<ProductBrandPriceGroup>()
                .Property(p => p.PriceGroupId)
                .ValueGeneratedOnAdd();

            //Relation n:1 with brand
            modelBuilder.Entity<ProductBrandPriceGroup>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.ProductBrandPriceGroups)
                .HasForeignKey(p => p.BrandId);

            //Relation n:1 with Product
            modelBuilder.Entity<ProductBrandPriceGroup>()
                .HasOne(p => p.Product)
                .WithMany(pr => pr.ProductBrandPriceGroups)
                .HasForeignKey(p => p.ProductId);

            //Relation n:1 with Size Group
            modelBuilder.Entity<ProductBrandPriceGroup>()
                .HasOne(p => p.SizeGroup)
                .WithMany(sg => sg.ProductBrandPriceGroups)
                .HasForeignKey(p => p.SizeGroupId);
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
                .HasOne(p => p.ProductBrandPriceGroup)
                .WithMany(pb => pb.ProductVariants)
                .HasForeignKey(p => p.PriceGroupId);

            #endregion


            #region Size configurations
            //Indicate the primary key
            modelBuilder.Entity<Size>()
                .HasKey(s => s.SizeId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<Size>()
                .Property(s => s.SizeId)
                .ValueGeneratedOnAdd();

            //Relation n:1 with SizeGroup
            modelBuilder.Entity<Size>()
                .HasOne(s => s.SizeGroup)
                .WithMany(sg => sg.Sizes)
                .HasForeignKey(s => s.SizeGroupId);
            #endregion


            #region SizeGroup configurations
            //Indicate the primary key
            modelBuilder.Entity<SizeGroup>()
                .HasKey(s => s.SizeGroupId);

            //Indicate that is a value generated on Add(Identity)
            modelBuilder.Entity<SizeGroup>()
                .Property(s => s.SizeGroupId)
                .ValueGeneratedOnAdd();
            #endregion

        }
    }
}
