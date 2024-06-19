using jcBENCH.MVC.Controllers.API.Base;
using jcBENCH.MVC.DAL;
using jcBENCH.MVC.Objects;
using Microsoft.AspNetCore.Mvc;
using Results = jcBENCH.MVC.DAL.Objects.Results;

namespace jcBENCH.MVC.Controllers.API
{
    [Route("api/resultsubmission")]
    public class ResultSubmissionController(MainDbContext dbContext) : BaseApiController
    {
        [HttpPost]
        public async Task<bool> SubmitAsync([FromBody] ResultSubmissionItem submissionItem)
        {
            var resultItem = new Results
            {
                BenchmarkResult = submissionItem.score,
                CPUArchitecture = submissionItem.cpu_architecture,
                CPUName = submissionItem.cpu_name,
                OperatingSystem = submissionItem.os_name,
                BenchmarkName = submissionItem.benchmark_name,
                CPUCoreCount = submissionItem.cpu_cores,
                BenchmarkThreadingModel = submissionItem.benchmark_threading_model,
                BenchmarkAPIVersion = submissionItem.benchmark_api_version
            };

            var _ = await dbContext.BenchmarkResults.AddAsync(resultItem);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}