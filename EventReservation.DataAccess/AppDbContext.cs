using EventReservation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventReservation.DataAccess;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var boolConverter = new BoolToZeroOneConverter<int>();

        // Apply Oracle-safe conversions
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(bool))
                {
                    property.SetValueConverter(boolConverter);
                    property.SetColumnType("NUMBER(1)");
                }

                if (property.ClrType == typeof(Guid))
                {
                    property.SetColumnType("RAW(16)");
                }

                if (property.ClrType == typeof(DateTimeOffset))
                {
                    property.SetColumnType("TIMESTAMP WITH TIME ZONE");
                }
            }
        }
    }
}