using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proiect1.Models;

namespace proiect1.Data
{
    public class proiect1Context : DbContext
    {
        public proiect1Context (DbContextOptions<proiect1Context> options)
            : base(options)
        {
        }

        public DbSet<proiect1.Models.Recipe> Recipe { get; set; } = default!;

        public DbSet<proiect1.Models.Category>? Category { get; set; }

        public DbSet<proiect1.Models.Ingredient>? Ingredient { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<RecipePhoto> RecipePhotos { get; set; }
        public DbSet<SavedRecipe> SavedRecipes { get; set; }
        public DbSet<proiect1.Models.User>? User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeIngredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeID);

            modelBuilder.Entity<RecipePhoto>()
            .ToTable("RecipePhotos")
            .HasKey(rp => rp.Id);

            modelBuilder.Entity<Recipe>()
          .HasOne(r => r.SavedRecipe)
          .WithOne(sr => sr.Recipe)
          .HasForeignKey<SavedRecipe>(sr => sr.RecipeId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SavedRecipe>()
                .HasIndex(sr => sr.RecipeId)
                .IsUnique(false); // Set IsUnique to false

            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedRecipes)
                .WithOne(sr => sr.User)
                .HasForeignKey(sr => sr.UserId)
                .IsRequired(false);


        }

    }

    }

