using Entities;
using MediumClone.Models;
using MediumClone.Models.Authentication;
using MediumClone.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository, ICategoryRepository categoryRepository, UserManager<AppUser> userManager)
        {
            _logger = logger;
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            ArticlesForMainPageVM articlesForMainPageVM = new ArticlesForMainPageVM();
            articlesForMainPageVM.Articles = articleRepository.GetAllIncludeAuthors();
            articlesForMainPageVM.TopViewedArticles = articleRepository.GetTrendingArticles(100);
            
            return View(articlesForMainPageVM);
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> UserIndex(AppUser user)
        {               
            ArticlesForMainPageVM articlesForMainPageVM = new ArticlesForMainPageVM();
            articlesForMainPageVM.Id = user.Id;
            articlesForMainPageVM.Articles = articleRepository.GetAll();
            articlesForMainPageVM.TopViewedArticles = articleRepository.GetTop10Articles();            
            user.Categories = categoryRepository.GetCategoriesById(user.Id).ToList();
            if (user.Categories != null && user.Categories.Count > 0)
            {
            List<int> categoryIdList = new List<int>();
            foreach (Category item in user.Categories){ categoryIdList.Add(item.Id);}
            articlesForMainPageVM.InterestedArticles = articleRepository.GetAllArtricleByInterestedIn(categoryIdList);
            }     
            return View(articlesForMainPageVM);     
        }
        public IActionResult AddCategoryToUser()
        {
            ArticlesForMainPageVM articlesForMainPageVM=new ArticlesForMainPageVM();
            articlesForMainPageVM.Categories=categoryRepository.GetAll();  
            return PartialView("_AddCategoryToAuthorPartial", articlesForMainPageVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategoryToUser(ArticlesForMainPageVM articlesForMainPageVM,string id)
        {
            AppUser user = await userManager.FindByIdAsync(HttpContext.Session.GetString("userID"));
            Category category = categoryRepository.GetById(articlesForMainPageVM.CategoryID);            
            user.Categories.Add(category);
            IdentityResult result =await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json("Ok");
            }
            else
            {
                ModelState.AddModelError("Category", "Somethings were wrong in update process..");
                 return Json("Fail");
            }
        }
        public IActionResult AddArticle()
        {
            NewArticleVM newArticleVM = new NewArticleVM();
            newArticleVM.Categories=categoryRepository.GetAll();            
            return View(newArticleVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle(NewArticleVM newArticleVM)
        {
            if (ModelState.IsValid)
            {
            bool check;
            Article article =new Article();
            article.Title = newArticleVM.Title;
            article.Content=newArticleVM.Content;           
            article.Author = await userManager.FindByIdAsync(HttpContext.Session.GetString("userID"));            
            foreach (int item in newArticleVM.CategoryIds)
            {
                article.Categories.Add(categoryRepository.GetById(item));
            }
           check= articleRepository.Add(article);
            if (check)
            {

            return RedirectToAction("UserIndex","Home",article.Author);
            }
            return View(newArticleVM);
            }
            newArticleVM.Categories = categoryRepository.GetAll();
            return View(newArticleVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
