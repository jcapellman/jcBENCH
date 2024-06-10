using jcBENCH.MVC.DAL;

using Microsoft.AspNetCore.Mvc;

namespace jcBENCH.MVC.Controllers
{
    public class HomeController(MainDbContext dbContext) : Controller
    {
        private readonly MainDbContext _dbContext = dbContext;

        public ActionResult Downloads() => View();

        public ActionResult About() => View();

        public ActionResult Index() => View(_dbContext.BenchmarkResults.OrderByDescending(a => a.BenchmarkResult).ToList());

        public ActionResult Archives() => View(_dbContext.BenchmarkResults.OrderBy(a => a.BenchmarkName).ToList());
    }
}