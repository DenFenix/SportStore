using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductID =1, Name = "P1"},
                 new Product{ProductID =2, Name = "P2"},
                 new Product{ProductID =3, Name = "P3"},
                 new Product{ProductID =4, Name = "P4"},
                 new Product{ProductID =5, Name = "P5"}
            }
            ).AsQueryable<Product>());
            ProductController productController = new ProductController(mock.Object);
            IEnumerable<Product> result = productController.List(2).ViewData.Model as IEnumerable<Product>;
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 1);
            Assert.Equal("P5", prodArray[0].Name);
        }
    }
}
