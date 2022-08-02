using Entities;
using MediumClone.Models;
using MediumClone.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MediumClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository articleRepository;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            this.articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            ArticlesForMainPageVM articlesForMainPageVM = new ArticlesForMainPageVM();
            articlesForMainPageVM.Articles = articleRepository.GetAllIncludeAuthors();
            articlesForMainPageVM.TopViewedArticles = articleRepository.GetWhere(a => a.ViewsCount >= 100);//
            return View(articlesForMainPageVM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
