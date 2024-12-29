
using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Domain.Entities;
using System.Reflection;

namespace Rira_TohidBadri_Crud.Infrastructure;

public class CrudDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

