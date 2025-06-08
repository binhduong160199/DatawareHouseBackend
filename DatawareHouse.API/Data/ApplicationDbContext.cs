using DatawareHouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatawareHouse.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<WarehouseRack> WarehouseRacks => Set<WarehouseRack>();
        public DbSet<RackPosition> RackPositions => Set<RackPosition>();
        public DbSet<Part> Parts => Set<Part>();
        public DbSet<Package> Packages => Set<Package>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<WorkerPosition> WorkerPositions => Set<WorkerPosition>();
        public DbSet<MovementLog> MovementLogs => Set<MovementLog>();
        public DbSet<ScanSession> ScanSessions => Set<ScanSession>();
        public DbSet<InventoryMismatch> InventoryMismatches => Set<InventoryMismatch>();
        public DbSet<SuggestedPlacement> SuggestedPlacements => Set<SuggestedPlacement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table names
            modelBuilder.Entity<WarehouseRack>().ToTable("warehouse_racks");
            modelBuilder.Entity<RackPosition>().ToTable("rack_positions");
            modelBuilder.Entity<Part>().ToTable("parts");
            modelBuilder.Entity<Package>().ToTable("packages");
            modelBuilder.Entity<Worker>().ToTable("workers");
            modelBuilder.Entity<WorkerPosition>().ToTable("worker_positions");
            modelBuilder.Entity<MovementLog>().ToTable("movement_logs");
            modelBuilder.Entity<ScanSession>().ToTable("scan_sessions");
            modelBuilder.Entity<InventoryMismatch>().ToTable("inventory_mismatches");
            modelBuilder.Entity<SuggestedPlacement>().ToTable("suggested_placements");

            // Primary Keys
            modelBuilder.Entity<WarehouseRack>().HasKey(e => e.Id);
            modelBuilder.Entity<RackPosition>().HasKey(e => e.Id);
            modelBuilder.Entity<Part>().HasKey(e => e.Id);
            modelBuilder.Entity<Package>().HasKey(e => e.Id);
            modelBuilder.Entity<Worker>().HasKey(e => e.Id);
            modelBuilder.Entity<WorkerPosition>().HasKey(e => e.Id);
            modelBuilder.Entity<MovementLog>().HasKey(e => e.Id);
            modelBuilder.Entity<ScanSession>().HasKey(e => e.Id);
            modelBuilder.Entity<InventoryMismatch>().HasKey(e => e.Id);
            modelBuilder.Entity<SuggestedPlacement>().HasKey(e => e.Id);

            // Relationships
            modelBuilder.Entity<RackPosition>()
                .HasOne(rp => rp.Rack)
                .WithMany(r => r.Positions)
                .HasForeignKey(rp => rp.RackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(p => p.Part)
                .WithMany(p => p.Packages)
                .HasForeignKey(p => p.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(p => p.RackPosition)
                .WithOne(rp => rp.Package)
                .HasForeignKey<Package>(p => p.RackPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkerPosition>()
                .HasOne(wp => wp.Worker)
                .WithMany()
                .HasForeignKey(wp => wp.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovementLog>()
                .HasOne(m => m.Package)
                .WithMany()
                .HasForeignKey(m => m.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            modelBuilder.Entity<Part>()
                .HasIndex(p => p.PartNumber).IsUnique();

            modelBuilder.Entity<RackPosition>()
                .HasIndex(p => p.PositionCode).IsUnique();

            modelBuilder.Entity<Worker>()
                .HasIndex(w => w.EmployeeCode).IsUnique();

            modelBuilder.Entity<Package>()
                .HasIndex(p => new { p.RackPositionId, p.PartId }).IsUnique();

            modelBuilder.Entity<ScanSession>()
                .HasIndex(s => new { s.ScannedPositionCode, s.ScannedPartNumber });

            modelBuilder.Entity<InventoryMismatch>()
                .HasIndex(m => m.PartNumber);

            modelBuilder.Entity<SuggestedPlacement>()
                .HasIndex(s => s.PartId);
        }
    }
}