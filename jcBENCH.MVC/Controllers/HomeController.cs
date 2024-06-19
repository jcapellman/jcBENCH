using jcBENCH.MVC.DAL;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jcBENCH.MVC.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController(MainDbContext dbContext) : Controller
    {
        [Route("downloads")]
        public ActionResult Downloads()
        {
            var releases = dbContext.Releases.Include(a => a.ReleaseArtifacts).OrderByDescending(a => a.ReleaseDate).ToList();

            return View(releases);
        }

        [Route("about")]
        public ActionResult About() => View();

        [Route("")]
        public ActionResult Index() => View(dbContext.BenchmarkResults.OrderByDescending(a => a.BenchmarkResult).ToList());

        [Route("archives")]
        public ActionResult Archives() => View(dbContext.BenchmarkResults.OrderBy(a => a.BenchmarkName).ToList());
    }
}