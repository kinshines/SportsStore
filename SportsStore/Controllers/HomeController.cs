using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index(string category, int pg = 1)
        {
            var viewModel = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((pg - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pg,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    repository.Products.Count() :
                    repository.Products.Count(e => e.Category == category)
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}
