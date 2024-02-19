using MarnaVblog.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics;

namespace MarnaVblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IBlogPostService _blogPostService;

        public HomeController(ILogger<HomeController> logger, IBlogPostService blogPostService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogPostService.GetAllBlogPostsAsync();
            return View(blogs);
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
