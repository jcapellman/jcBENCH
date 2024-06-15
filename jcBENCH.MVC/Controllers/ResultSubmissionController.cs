using jcBENCH.MVC.DAL;
using jcBENCH.MVC.Objects;
using Microsoft.AspNetCore.Mvc;
using Results = jcBENCH.MVC.DAL.Objects.Results;

namespace jcBENCH.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultSubmissionController(MainDbContext dbContext) : ControllerBase
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

            var result = await dbContext.BenchmarkResults.AddAsync(resultItem);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}