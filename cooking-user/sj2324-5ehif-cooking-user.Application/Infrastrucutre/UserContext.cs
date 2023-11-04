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

        // TODO: Replace with data from env file
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
