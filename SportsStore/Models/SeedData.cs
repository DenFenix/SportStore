using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Ball", Description = "FootBall", Category = "Soccer", Price = 34.9m },
                    new Product { Name = "Flags", Description = "FootBallFlags", Category = "Soccer", Price = 22.9m },
                    new Product { Name = "Ball1", Description = "FootBall1", Category = "Soccer1", Price = 34.9m },
                    new Product { Name = "Flags1", Description = "FootBallFlags1", Category = "Soccer1", Price = 22.9m },
                    new Product { Name = "Ball2", Description = "FootBall2", Category = "Soccer2", Price = 34.9m },
                    new Product { Name = "Flags2", Description = "FootBallFlags2", Category = "Soccer2", Price = 22.9m });
                context.SaveChanges();
            }
        }
    }
}
