using apbd_cw7_git_s33418.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw7_git_s33418.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Component> Components { get; set; } = null!;
        public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; } = null!;
        public DbSet<ComponentType> ComponentTypes { get; set; } = null!;
        public DbSet<PC> PCs { get; set; } = null!;
        public DbSet<PCComponent> PCComponents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentManufacturer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Abbreviation)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(c => c.FullName)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(c => c.FoundationDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.HasData(
                    new ComponentManufacturer { Id = 1, Abbreviation = "INTL", FullName = "Intel Corporation", FoundationDate = new DateTime(1968, 7, 18) },
                    new ComponentManufacturer { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices, Inc.", FoundationDate = new DateTime(1969, 5, 1) },
                    new ComponentManufacturer { Id = 3, Abbreviation = "NVDA", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
                    new ComponentManufacturer { Id = 4, Abbreviation = "CRSR", FullName = "Corsair Gaming, Inc.", FoundationDate = new DateTime(1994, 1, 1) },
                    new ComponentManufacturer { Id = 5, Abbreviation = "SMSNG", FullName = "Samsung Electronics Co., Ltd.", FoundationDate = new DateTime(1969, 1, 13) },
                    new ComponentManufacturer { Id = 6, Abbreviation = "ASUS", FullName = "ASUSTeK Computer Inc.", FoundationDate = new DateTime(1989, 4, 2) },
                    new ComponentManufacturer { Id = 7, Abbreviation = "SSNC", FullName = "Sea Sonic Electronics Co., Ltd.", FoundationDate = new DateTime(1975, 9, 1) }
                );
            });

            modelBuilder.Entity<ComponentType>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Abbreviation)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(c => c.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.HasData(
                    new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
                    new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
                    new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" },
                    new ComponentType { Id = 4, Abbreviation = "SSD", Name = "Storage Drive" },
                    new ComponentType { Id = 5, Abbreviation = "MB", Name = "Motherboard" },
                    new ComponentType { Id = 6, Abbreviation = "PSU", Name = "Power Supply" }
                );
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(c => c.Code);

                entity.Property(c => c.Code)
                    .HasColumnType("char(10)")
                    .IsRequired();

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(c => c.Description)
                    .IsRequired();

                entity.HasOne(c => c.ComponentManufacturer)
                    .WithMany(i => i.Components)
                    .HasForeignKey(c => c.ComponentManufacturerId)
                    .IsRequired();

                entity.HasOne(c => c.ComponentType)
                    .WithMany(i => i.Components)
                    .HasForeignKey(c => c.ComponentTypeId)
                    .IsRequired();

                entity.HasData(
                    new Component { Code = "CPU001", Name = "Intel Core i5-14600K", Description = "14-core desktop processor for gaming and productivity.", ComponentManufacturerId = 1, ComponentTypeId = 1 },
                    new Component { Code = "CPU002", Name = "AMD Ryzen 7 7800X3D", Description = "8-core processor with 3D V-Cache for gaming builds.", ComponentManufacturerId = 2, ComponentTypeId = 1 },
                    new Component { Code = "GPU001", Name = "GeForce RTX 4070 Super", Description = "Graphics card for high-refresh 1440p gaming.", ComponentManufacturerId = 3, ComponentTypeId = 2 },
                    new Component { Code = "GPU002", Name = "Radeon RX 7800 XT", Description = "Graphics card with 16 GB memory for gaming and creation.", ComponentManufacturerId = 2, ComponentTypeId = 2 },
                    new Component { Code = "RAM001", Name = "Corsair Vengeance 32GB DDR5", Description = "Two 16 GB DDR5 memory modules.", ComponentManufacturerId = 4, ComponentTypeId = 3 },
                    new Component { Code = "SSD001", Name = "Samsung 990 Pro 2TB", Description = "Fast NVMe SSD for system and project storage.", ComponentManufacturerId = 5, ComponentTypeId = 4 },
                    new Component { Code = "MB001", Name = "ASUS TUF Gaming B650-Plus", Description = "AM5 motherboard for Ryzen processors.", ComponentManufacturerId = 6, ComponentTypeId = 5 },
                    new Component { Code = "PSU001", Name = "Seasonic Focus GX-750", Description = "750 W modular power supply.", ComponentManufacturerId = 7, ComponentTypeId = 6 }
                );
            });

            modelBuilder.Entity<PC>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(c => c.Weight)
                    .IsRequired();

                entity.Property(c => c.Warranty)
                    .IsRequired();

                entity.Property(c => c.CreatedAt)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(c => c.Stock)
                    .IsRequired();

                entity.ToTable(i => i.HasCheckConstraint("CK_Product_Stock_NonNegative", "[Stock] >= 0"));
                entity.ToTable(i => i.HasCheckConstraint("CK_Product_Weight_NonNegative", "[Weight] >= 0"));
                entity.ToTable(i => i.HasCheckConstraint("CK_Product_Warranty_NonNegative", "[Warranty] >= 0"));

                entity.HasData(
                    new PC { Id = 1, Name = "Office Mini", Weight = 4.2f, Warranty = 24, CreatedAt = new DateTime(2026, 1, 10, 9, 0, 0), Stock = 18 },
                    new PC { Id = 2, Name = "Creator Pro", Weight = 9.8f, Warranty = 36, CreatedAt = new DateTime(2026, 2, 15, 10, 30, 0), Stock = 7 },
                    new PC { Id = 3, Name = "Gaming X", Weight = 11.4f, Warranty = 36, CreatedAt = new DateTime(2026, 3, 20, 14, 45, 0), Stock = 11 },
                    new PC { Id = 4, Name = "Workstation Max", Weight = 13.1f, Warranty = 48, CreatedAt = new DateTime(2026, 4, 5, 12, 0, 0), Stock = 4 }
                );
            });

            modelBuilder.Entity<PCComponent>(entity =>
            {
                entity.HasKey(e => new { e.PCId, e.ComponentCode });

                entity.Property(e => e.ComponentCode)
                    .HasColumnType("char(10)")
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .IsRequired();

                entity.HasOne(e => e.PC)
                    .WithMany(i => i.PCComponents)
                    .HasForeignKey(e => e.PCId);

                entity.HasOne(e => e.Component)
                    .WithMany(i => i.PCComponents)
                    .HasForeignKey(i => i.ComponentCode);

                entity.HasData(
                    new PCComponent { PCId = 1, ComponentCode = "CPU001", Amount = 1 },
                    new PCComponent { PCId = 1, ComponentCode = "RAM001", Amount = 1 },
                    new PCComponent { PCId = 1, ComponentCode = "SSD001", Amount = 1 },
                    new PCComponent { PCId = 2, ComponentCode = "CPU002", Amount = 1 },
                    new PCComponent { PCId = 2, ComponentCode = "GPU002", Amount = 1 },
                    new PCComponent { PCId = 2, ComponentCode = "RAM001", Amount = 2 },
                    new PCComponent { PCId = 2, ComponentCode = "SSD001", Amount = 2 },
                    new PCComponent { PCId = 3, ComponentCode = "CPU001", Amount = 1 },
                    new PCComponent { PCId = 3, ComponentCode = "GPU001", Amount = 1 },
                    new PCComponent { PCId = 3, ComponentCode = "RAM001", Amount = 1 },
                    new PCComponent { PCId = 3, ComponentCode = "MB001", Amount = 1 },
                    new PCComponent { PCId = 3, ComponentCode = "PSU001", Amount = 1 },
                    new PCComponent { PCId = 4, ComponentCode = "CPU002", Amount = 1 },
                    new PCComponent { PCId = 4, ComponentCode = "GPU001", Amount = 2 },
                    new PCComponent { PCId = 4, ComponentCode = "RAM001", Amount = 4 },
                    new PCComponent { PCId = 4, ComponentCode = "SSD001", Amount = 2 },
                    new PCComponent { PCId = 4, ComponentCode = "PSU001", Amount = 1 }
                );
            });
        }
    }
}
