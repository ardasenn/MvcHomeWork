﻿using Entities;
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
        //[Authorize(Roles ="User,Admin")]
        [HttpGet]
        public async Task<IActionResult> UserIndex(AppUser user)
        {               
            ArticlesForMainPageVM articlesForMainPageVM = new ArticlesForMainPageVM();
            articlesForMainPageVM.Id = user.Id;
            articlesForMainPageVM.Articles = articleRepository.GetAllIncludeAuthors();
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
       // [Authorize(Roles = "User,Admin")]
        public IActionResult AddCategoryToUser()
        {
            ArticlesForMainPageVM articlesForMainPageVM=new ArticlesForMainPageVM();
            articlesForMainPageVM.Categories=categoryRepository.GetAll();  
            return PartialView("_AddCategoryToAuthorPartial", articlesForMainPageVM);
        }
       // [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategoryToUser(ArticlesForMainPageVM articlesForMainPageVM,string id)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
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
       // [Authorize]
        public IActionResult AddArticle()
        {
            NewArticleVM newArticleVM = new NewArticleVM();
            newArticleVM.AllCategories = categoryRepository.GetAll();            
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
            string[] words = article.Content.Split(' ');
            article.ReadTime = words.Length/ 220;              
             article.Author = await userManager.GetUserAsync(HttpContext.User);
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
            newArticleVM.AllCategories = categoryRepository.GetAll();
            return View(newArticleVM);
        }

        public IActionResult UpdateArticle(int id)
        {
            Article article= articleRepository.GetArticleByIdWithCategories(id);
            NewArticleVM newArticleVM = new NewArticleVM();           
            newArticleVM.AllCategories = categoryRepository.GetAll();
            newArticleVM.CategoriesArticle = article.Categories;
            newArticleVM.Title = article.Title;
            newArticleVM.Content = article.Content;
            newArticleVM.ArticleId = id;
            return View(newArticleVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArticle(NewArticleVM newArticleVM,int id)
        {
            if (ModelState.IsValid)
            {
                bool check;
                Article article = articleRepository.GetArticleByIdWithCategories(id);
                article.Title = newArticleVM.Title;
                article.Content = newArticleVM.Content;
                article.Author = await userManager.GetUserAsync(HttpContext.User);
                article.Categories.Clear();
                foreach (int item in newArticleVM.CategoryIds)
                {
                    article.Categories.Add(categoryRepository.GetById(item));
                }
                check = articleRepository.Update(article);
                if (check)
                {

                    return RedirectToAction("UserIndex", "Home", article.Author);
                }
                return View(newArticleVM);
            }
            
            return RedirectToAction("UpdateArticle",id);
        }

        public async Task<IActionResult> MyArticles()
        {
            AppUser appUser = await userManager.GetUserAsync(HttpContext.User);
            AuthorsArticlesVM authorsArticlesVM = new AuthorsArticlesVM();
            authorsArticlesVM.Articles = articleRepository.GetAllArticlesByAuthor(appUser.Id);
            //authorsArticlesVM.Categories=categoryRepository.
            authorsArticlesVM.UserId = appUser.Id;
            return View(authorsArticlesVM);
        }

        public IActionResult ArticleRead(int id)
        {
            Article article = articleRepository.GetArticleWithCategoriesAndAuthor(id);
            //article.Author = await userManager.GetUserAsync(HttpContext.User);
            article.ViewsCount += 1;
            articleRepository.Update(article);
            return View(article);
        }
        public  IActionResult DeleteArticle ( int id)
        {
            articleRepository.Delete(articleRepository.GetArticleByIdWithCategories(id));
            return RedirectToAction("MyArticles");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> UserPage(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            user.Articles = articleRepository.GetAllArticlesByAuthor(id).ToList();
            return View(user);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
