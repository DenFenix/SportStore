using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Selected_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                 new Product{ProductID =1, Name = "P1", Category ="C1"},
                 new Product{ProductID =2, Name = "P2", Category ="C2"},
                 new Product{ProductID =3, Name = "P3", Category ="C3"},
                 new Product{ProductID =4, Name = "P4", Category ="C2"},
                 new Product{ProductID =5, Name = "P5", Category ="C1"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            string[] result = ((IEnumerable<string>)(target.Invoke()
                as ViewViewComponentResult).ViewData.Model).ToArray();
            string[] descList = new String[] { "C1", "C2", "C3" };
            Assert.True(Enumerable.SequenceEqual(descList, result));
        }
        [Fact]
        public void Indicates_Selected_Category()
        {
            string categoryToSelected = "C1";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                 new Product{ProductID =1, Name = "P1", Category ="C1"},
                 new Product{ProductID =2, Name = "P2", Category ="C2"},
                 new Product{ProductID =3, Name = "P3", Category ="C3"},
                 new Product{ProductID =4, Name = "P4", Category ="C2"},
                 new Product{ProductID =5, Name = "P5", Category ="C1"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelected;
            string result = (string)(target.Invoke()
                as ViewViewComponentResult).ViewData["SelectedCategory"];
            string[] descList = new String[] { "C1", "C2", "C3" };
            Assert.Equal(categoryToSelected,result);
        }
    }
}
