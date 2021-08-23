using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private const int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int productPage = 1) =>
            View(_repository.Products
                .OrderBy(p=>p.ProductID)
                .Skip((productPage-1)*PageSize)
                .Take(PageSize));
    }
}
