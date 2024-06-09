using jcBENCH.MVC.DAL;
using jcBENCH.MVC.Objects;
using Microsoft.AspNetCore.Mvc;
using Results = jcBENCH.MVC.DAL.Objects.Results;

namespace jcBENCH.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultSubmissionController : ControllerBase
    {
        private readonly ILogger<ResultSubmissionController> _logger;

        private readonly MainDbContext _dbContext;

        public ResultSubmissionController(ILogger<ResultSubmissionController> logger, MainDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<bool> SubmitAsync([FromBody] ResultSubmissionItem submissionItem)
        {
            try
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

                var result = await _dbContext.BenchmarkResults.AddAsync(resultItem);

                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to submit {submissionItem}");
                return false;
            }
        }
    }
}
