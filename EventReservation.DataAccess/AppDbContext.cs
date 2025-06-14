using EventReservation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventReservation.DataAccess;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Event> Event { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<SessionLimit> SessionLimit { get; set; }
    public DbSet<Registration> Registration { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var boolConverter = new BoolToZeroOneConverter<int>();

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

        modelBuilder.Entity<AppUser>(b =>
        {
            b.Property(u => u.IntId)
             .HasColumnName("INT_ID")
             .ValueGeneratedOnAdd()
             .UseIdentityColumn();

            b.HasAlternateKey(u => u.IntId)
             .HasName("AK_AspNetUsers_IntId");
        });
        modelBuilder.Entity<Registration>(b =>
        {
            b.HasOne(r => r.AppUser)
             .WithMany()
             .HasForeignKey(r => r.UserId)
             .HasPrincipalKey(u => u.IntId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}