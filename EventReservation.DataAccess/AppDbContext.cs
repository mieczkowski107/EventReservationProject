using EventReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace EventReservation.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}