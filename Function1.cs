using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FuncForDennis
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            System.ServiceModel.BasicHttpBinding httpThingy = new System.ServiceModel.BasicHttpBinding();

            //log.LogInformation(httpThingy);

            //public EZLynxVINSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
            //    base(EZLynxVINSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
            //{
            //    this.Endpoint.Name = endpointConfiguration.ToString();
            //    ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
            //}


            //public VINService(IConfiguration azureAppConfiguration)

            //{

            //    _configuration = azureAppConfiguration;

            //    _ratingSettings = azureAppConfiguration.GetSection(RatingSettings.SectionName).Get<RatingSettings>();

            //    _soapClient = new EZLynxVINSoapClient(EZLynxVINSoapClient.EndpointConfiguration.EZLynxVINSoap, _ratingSettings.EZLynxVinServiceBaseURL);

            //    _authHeader = new AuthenticationHeader() { Username = _ratingSettings.EZLynxUsername, Password = _ratingSettings.EZLynxPassword };

            //}



            //public async Task<List<YearDTO>> GetVehicleYears()

            //{



            //    var years = await _soapClient.getAutoYearsAsync(_authHeader);



            //    var pairs = years.getAutoYearsResult.Select(x => new YearDTO()

            //    {

            //        Year = int.Parse(x),

            //        YearDisplay = x.ToString()

            //    }).OrderByDescending(z => z.Year).ToList();



            //    return pairs;

            //}

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
