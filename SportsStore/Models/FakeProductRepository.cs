using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> 
        { 
            new Product { ProductID = 1, Category = "test", Description = "test2", Name = "Name1", Price = 1599 },
            new Product { ProductID = 2, Category = "test", Description = "test3", Name = "Name2", Price = 25599 }
        }
        .AsQueryable<Product>();
    }
}
