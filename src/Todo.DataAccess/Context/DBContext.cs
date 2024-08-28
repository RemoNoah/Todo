using Todo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Todo.DataAccess.Context;

/// <summary>
/// Represents the Entity Framework database context for the Todo application.
/// This class inherits from <see cref="DbContext"/> and provides access to the database entities.
/// </summary>
public class TodoContext : DbContext
{
    private readonly IConfiguration? _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoContext" /> class.
    /// </summary>
    /// <remarks>
    /// See <a href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</a> for more information.
    /// </remarks>
    public TodoContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public TodoContext(DbContextOptions<TodoContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlServer(_configuration!.GetConnectionString("Default"));
        }
    }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public virtual DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public virtual DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data
        _ = modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" },
            new Role { Id = Guid.NewGuid(), Name = "Client" }
        );
    }
}
