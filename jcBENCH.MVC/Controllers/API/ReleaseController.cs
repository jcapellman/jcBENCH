using jcBENCH.MVC.Controllers.API.Base;
using jcBENCH.MVC.DAL;
using jcBENCH.MVC.DAL.Objects;
using jcBENCH.MVC.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jcBENCH.MVC.Controllers.API
{
    [Route("/api/release")]
    public class ReleaseController(MainDbContext dbContext) : BaseApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<bool> SubmitNewRelease(ReleaseRequestItem requestItem)
        {
            var release = new Releases
            {
                Description = requestItem.Description,
                IsPreRelease = requestItem.IsPreRelease,
                Name = requestItem.Name,
                ReleaseDate = requestItem.ReleaseDate
            };

            foreach (var releaseArtifact in requestItem.Artifacts.Select(artifact => new ReleaseArtifacts
            {
                Architecture = artifact.Architecture,
                Description = artifact.Description,
                DownloadURI = artifact.DownloadURI,
                OperatingSystem = artifact.OperatingSystem
            }))
            {
                release.ReleaseArtifacts.Add(releaseArtifact);
            }

            await dbContext.Releases.AddAsync(release);

            return await dbContext.SaveChangesAsync() > 0;
        }

        [Authorize]
        [HttpPost]
        [Route("{releaseId}/")]
        public async Task<bool> AddNewReleaseArtifact(int releaseId, ReleaseArtifactRequestItem artifact)
        {
            var releaseArtifact = new ReleaseArtifacts
            {
                Architecture = artifact.Architecture,
                Description = artifact.Description,
                DownloadURI = artifact.DownloadURI,
                OperatingSystem = artifact.OperatingSystem,
                ReleaseID = releaseId
            };

            await dbContext.ReleaseArtifacts.AddAsync(releaseArtifact);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}