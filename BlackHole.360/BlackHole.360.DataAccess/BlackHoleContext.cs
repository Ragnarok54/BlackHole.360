using BlackHole._360.Domain.Entities;
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

    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Department> Departments => Set<Department>();
    public virtual DbSet<Group> Groups => Set<Group>();
    public virtual DbSet<SubGroup> SubGroups => Set<SubGroup>();
}
