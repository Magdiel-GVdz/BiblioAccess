using ITS.BiblioAccess.Domain.Entities;
using ITS.BiblioAccess.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.BiblioAccess.Infrastructure.Data;

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
        modelBuilder.Entity<Career>(entity =>
        {
            entity.HasKey(c => c.CareerId);
            entity.Property(c => c.CareerId).ValueGeneratedNever();

            entity.Property(c => c.Name)
                .HasConversion(
                    name => name.Value,
                    value => CareerName.Create(value).Value
                )
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.IsActive)
                .IsRequired();
        });

        // Configuración de EntryRecord
        modelBuilder.Entity<EntryRecord>(entity =>
        {
            entity.HasKey(e => e.EntryId);
            entity.Property(e => e.EntryId).ValueGeneratedNever();

            entity.Property(e => e.Timestamp)
                .IsRequired();

            entity.Property(e => e.UserType)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(e => e.Gender)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(e => e.CareerId)
                .IsRequired(false);

            entity.HasOne<Career>()
                .WithMany()
                .HasForeignKey(e => e.CareerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de SystemConfiguration
        modelBuilder.Entity<SystemConfiguration>(entity =>
        {
            entity.HasKey(s => s.SystemConfigurationId);
            entity.Property(s => s.SystemConfigurationId).ValueGeneratedNever();

            entity.Property(s => s.ExportHour)
                .HasConversion(
                    hour => hour.Value,
                    value => ExportHour.Create(value).Value
                )
                .IsRequired();
        });
    }
}
