using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlackHole._360.DataAccess;
public class BlackHoleContext : DbContext
{
    public BlackHoleContext(DbContextOptions<BlackHoleContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
