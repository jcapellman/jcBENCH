using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using jcBENCH.lib.Common;
using jcBENCH.lib.Objects;

using Newtonsoft.Json;

namespace jcBENCH.lib.Handlers
{
    public class SubmissionHandler
    {
        public async Task<bool> SubmitResultsAsync(ResultSubmissionItem submissionItem)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Constants.WEB_SERVICE_URL);

                    var content = new StringContent(JsonConvert.SerializeObject(submissionItem), Encoding.UTF8, "application/json");

                    var response = httpClient.PostAsync("ResultSubmission", content).Result;
                    
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return Convert.ToBoolean(responseBody);
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Error Submitting: {ex}");

                return false;
            }
        }
    }
}