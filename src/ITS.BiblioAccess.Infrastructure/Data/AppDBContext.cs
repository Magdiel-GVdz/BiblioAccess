using Microsoft.EntityFrameworkCore;
using ITS.BiblioAccess.Domain.Entities;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public DbSet<Career> Careers { get; set; }
    public DbSet<EntryRecord> EntryRecords { get; set; }
    public DbSet<SystemConfiguration> SystemConfigurations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Career
        modelBuilder.Entity<Career>()
            .HasKey(c => c.CareerId);

        modelBuilder.Entity<Career>()
            .Property(c => c.CareerId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Career>()
            .OwnsOne(c => c.Name, n =>
            {
                n.Property(nv => nv.Value)
                 .HasColumnName("Name")
                 .HasMaxLength(100)
                 .IsRequired();
            });

        // Configuración de EntryRecord
        modelBuilder.Entity<EntryRecord>()
            .HasKey(e => e.EntryId);

        modelBuilder.Entity<EntryRecord>()
            .Property(e => e.EntryId)
            .ValueGeneratedNever();

        modelBuilder.Entity<EntryRecord>()
            .Property(e => e.Timestamp)
            .IsRequired();

        modelBuilder.Entity<EntryRecord>()
            .Property(e => e.UserType)
            .HasConversion<int>() // Guardamos el Enum como entero
            .IsRequired();

        modelBuilder.Entity<EntryRecord>()
            .Property(e => e.Gender)
            .HasConversion<int>()
            .IsRequired();

        // Configuración de SystemConfiguration
        modelBuilder.Entity<SystemConfiguration>()
            .HasKey(s => s.SystemConfigurationId);

        modelBuilder.Entity<SystemConfiguration>()
            .Property(s => s.SystemConfigurationId)
            .ValueGeneratedNever();

        modelBuilder.Entity<SystemConfiguration>()
            .OwnsOne(s => s.ExportHour, eh =>
            {
                eh.Property(ehv => ehv.Value)
                  .HasColumnName("ExportHour")
                  .IsRequired();
            });
    }
}
