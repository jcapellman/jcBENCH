using jcBENCH.MVC.DAL;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC.Controllers
{
    public class HomeController(MainDbContext dbContext) : Controller
    {
        public ActionResult Downloads()
        {
            var releases = dbContext.Releases.Include(a => a.ReleaseArtifacts).OrderByDescending(a => a.ReleaseDate).ToList();

            return View(releases);
        }

        public ActionResult About() => View();

        public ActionResult Index() => View(dbContext.BenchmarkResults.OrderByDescending(a => a.BenchmarkResult).ToList());

        public ActionResult Archives() => View(dbContext.BenchmarkResults.OrderBy(a => a.BenchmarkName).ToList());
    }
}