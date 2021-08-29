using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
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

        public ViewResult List(string category, int productPage = 1) =>
            View(new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p=> category == null||p.Category==category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null? _repository.Products.Count():
                    _repository.Products.Where(p=>p.Category==category).Count()
                },
                CurrentCategory = category

            });
    }
}
