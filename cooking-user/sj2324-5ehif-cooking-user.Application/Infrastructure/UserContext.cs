using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Infrastructure;

public class UserContext : DbContext
{
    public DbSet<Cookbook> Cookbooks => Set<Cookbook>();
    public DbSet<Preference> Preferences => Set<Preference>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<User> Users => Set<User>();

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cookbook>()
            .HasKey(r => r.Key);
        modelBuilder.Entity<Preference>()
            .HasKey(r => r.Key);
        modelBuilder.Entity<Recipe>()
            .HasKey(r => r.Key);
        modelBuilder.Entity<User>()
            .HasKey(r => r.Key);
    }
}