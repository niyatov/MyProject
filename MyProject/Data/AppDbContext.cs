using Microsoft.EntityFrameworkCore;
using MyProject.Entities;

namespace MyProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }
}
