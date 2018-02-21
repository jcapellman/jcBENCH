using System.Linq;

using jcBENCH.lib.Objects;
using jcBENCH.web.DAL;

using Microsoft.AspNetCore.Mvc;

namespace jcBENCH.web.Controllers
{
    public class HomeController : Controller
    {
        private MainDbContext dbContext;

        public HomeController(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var topResults = dbContext.Results.OrderByDescending(a => a.BenchmarkResult).Take(Common.Constants.TOP_RESULTS_LIMIT).Select(row => new TopResultsListingItem
            {
                BenchmarkResult = row.BenchmarkResult,
                CPUManufacturer = row.CPUManufacturer,
                CPUModelName = row.CPUName
            }).ToList();

            return View(topResults);
        }
    }
}