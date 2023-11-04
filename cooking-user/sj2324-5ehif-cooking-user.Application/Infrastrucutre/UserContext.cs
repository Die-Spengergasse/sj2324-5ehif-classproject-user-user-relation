using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user.Application.Model;

namespace sj2324_5ehif_cooking_user.Application.Infrastrucutre
{
    public class UserContext : DbContext
    {
        public DbSet<Cookbook> Cookbooks { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }

        // private static string dbUsername = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? throw new Exception();
        // private static string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? throw new Exception();

        // private string connectionString = $"Host=postgres;Database=db_user;Username={dbUsername};Password={dbPassword}";
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseNpgsql(connectionString);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=postgres;Database=user_db;Username=user;Password=pOLDUProPJ");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(e => e.Key, navigation =>
                {
                    navigation.HasIndex(nameof(UserKey._value)).IsUnique();
                });

                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Cookbook>(entity =>
            {
                entity.OwnsOne(e => e.Key, navigation =>
                {
                    navigation.HasIndex(nameof(CookbookKey._value)).IsUnique();
                });
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.OwnsOne(e => e.Key, navigation =>
                {
                    navigation.HasIndex(nameof(PreferenceKey._value)).IsUnique();
                });
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.OwnsOne(e => e.Key, navigation =>
                {
                    navigation.HasIndex(nameof(RecipeKey._value)).IsUnique();
                });
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
