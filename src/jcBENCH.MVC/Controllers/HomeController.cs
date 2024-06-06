using jcBENCH.MVC.DAL;
using jcBENCH.MVC.Objects;
using Microsoft.AspNetCore.Mvc;

namespace jcBENCH.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MainDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MainDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ActionResult Download() => View();

        public ActionResult About() => View();

        public ActionResult Index()
        {
            var topResults = _dbContext.BenchmarkResults.OrderByDescending(a => a.BenchmarkResult).ToList();

            return View(topResults);
        }
    }
}