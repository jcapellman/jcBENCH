using System.Diagnostics;

using jcBENCH.MVC.Common;
using jcBENCH.MVC.DAL;
using jcBENCH.MVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Octokit;

namespace jcBENCH.MVC.Controllers
{
    public class HomeController(MainDbContext dbContext) : Controller
    {
        [OutputCache(Duration = AppConstants.OutputCacheDurationSeconds)]
        public async Task<ActionResult> Downloads()
        {
            var client = new GitHubClient(new ProductHeaderValue(AppConstants.GitHubOwnerName));

            var releases = await client.Repository.Release.GetAll(AppConstants.GitHubOwnerName, AppConstants.GitHubRepositoryName);

            var downloads = releases.Where(a => a.Assets.Any() && a.PublishedAt is not null).Take(AppConstants.GitHubReleaseLimit)
                .Select(release =>
                {
                    Debug.Assert(release.PublishedAt != null, "release.PublishedAt != null");

                    return new ReleaseResponseItem
                    {
                        Description = release.Body,
                        Version = release.Name,
                        ReleaseDate = release.PublishedAt.Value,
                        Downloads = release.Assets.Select(a => new DownloadResponseItem
                            { Label = a.Name, URL = a.BrowserDownloadUrl }).ToList()
                    };
                }).ToList();

            return View(downloads);
        }

        public ActionResult About() => View();

        public ActionResult Index() => View(dbContext.BenchmarkResults.OrderByDescending(a => a.BenchmarkResult).ToList());

        public ActionResult Archives() => View(dbContext.BenchmarkResults.OrderBy(a => a.BenchmarkName).ToList());
    }
}