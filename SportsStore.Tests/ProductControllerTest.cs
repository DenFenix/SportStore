using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
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
            //IEnumerable<Product> result = productController.List(2).ViewData.Model as IEnumerable<Product>;
            ProductsListViewModel result = productController.List(null,2).ViewData.Model as ProductsListViewModel;
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 1);
            Assert.Equal("P5", prodArray[0].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                 new Product{ProductID =1, Name = "P1"},
                 new Product{ProductID =2, Name = "P2"},
                 new Product{ProductID =3, Name = "P3"},
                 new Product{ProductID =4, Name = "P4"},
                 new Product{ProductID =5, Name = "P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            ProductsListViewModel result = controller.List(null,2).ViewData.Model as ProductsListViewModel;
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(4, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Breakdown_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                 new Product{ProductID =1, Name = "P1", Category = "K2"},
                 new Product{ProductID =2, Name = "P2", Category = "K1"},
                 new Product{ProductID =3, Name = "P3", Category = "K1"},
                 new Product{ProductID =4, Name = "P4", Category = "K2"},
                 new Product{ProductID =5, Name = "P5", Category = "K1"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            ProductsListViewModel result = controller.List("K2", 1).ViewData.Model as ProductsListViewModel;
            PagingInfo pagingInfo = result.PagingInfo;
            Product[] prodArray = result.Products.ToArray();
            //Assert.Equal(1, pagingInfo.CurrentPage);
            //Assert.Equal(2, pagingInfo.TotalItems);
            //Assert.Equal(1, pagingInfo.TotalPages);
            Assert.Equal("K2", result.CurrentCategory);
            Assert.Equal(2, prodArray.Count());
            Assert.Equal(1, prodArray[0].ProductID);
            Assert.Equal(4, prodArray[1].ProductID);
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                 new Product{ProductID =1, Name = "P1", Category = "K2"},
                 new Product{ProductID =2, Name = "P2", Category = "K1"},
                 new Product{ProductID =3, Name = "P3", Category = "K1"},
                 new Product{ProductID =4, Name = "P4", Category = "K2"},
                 new Product{ProductID =5, Name = "P5", Category = "K1"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            Func<ViewResult, ProductsListViewModel> GetModel = result =>
            result?.ViewData?.Model as ProductsListViewModel;

            int? res1 = GetModel(controller.List("K1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(controller.List("K2"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(controller.List(null))?.PagingInfo.TotalItems;

            Assert.Equal(3,res1);
            Assert.Equal(2, res2);
            Assert.Equal(5, resAll);
        }
    }
}
