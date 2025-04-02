using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCADA.Common.Models;

namespace SCADA.Database.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PLC> PLCs { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure PLC entity
        builder.Entity<PLC>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.IPAddress).IsRequired();
            entity.Property(p => p.ProtocolType).IsRequired();
            entity.HasMany(p => p.Tags)
                  .WithOne()
                  .HasForeignKey(t => t.PlcId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Tag entity
        builder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Address).IsRequired();
            entity.Property(t => t.DataType).IsRequired();
        });
    }
}