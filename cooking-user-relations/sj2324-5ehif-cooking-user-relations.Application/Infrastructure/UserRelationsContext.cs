using Microsoft.EntityFrameworkCore;
using sj2324_5ehif_cooking_user_relations.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Infrastructure
{

        public class UserRelationsContext : DbContext
        {
            public DbSet<Feedback> Feedbacks => Set<Feedback>();
            public DbSet<Follow> Follows => Set<Follow>();
            public DbSet<RecipeShare> RecipeShares => Set<RecipeShare>();
            public DbSet<Recipe> Recipes => Set<Recipe>();
            public DbSet<User> Users => Set<User>();
            public UserRelationsContext(DbContextOptions<UserRelationsContext> options) : base(options)
            {
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            }
        }
    

}
