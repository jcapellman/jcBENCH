using System;
using System.Threading.Tasks;

using jcBENCH.lib.Objects;
using jcBENCH.web.DAL;
using jcBENCH.web.DAL.Objects;

using Microsoft.AspNetCore.Mvc;

namespace jcBENCH.web.Controllers
{
    [Route("api/[controller]")]
    public class ResultSubmissionController : Controller
    {
        private MainDbContext dbContext;

        public ResultSubmissionController(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<bool> SubmitAsync([FromBody]ResultSubmissionItem submissionItem)
        {
            var resultItem = new Results
            {
                BenchmarkID = submissionItem.BenchmarkID,
                BenchmarkResult = submissionItem.BenchmarkResult,
                CPUArchitecture = submissionItem.CPUArchitecture,
                CPUFrequency = submissionItem.CPUFrequency,
                CPUManufacturer = submissionItem.CPUManufacturer,
                CPUName = submissionItem.CPUName,
                OperatingSystem = submissionItem.OperatingSystem,
                PlatformID = submissionItem.PlatformID,
                Created = DateTime.Now
            };

            var result = await dbContext.Results.AddAsync(resultItem);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}