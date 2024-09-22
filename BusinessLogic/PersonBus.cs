using Domain.Entities;
using Infra;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic
{
    public class PersonBus
    {
        private IConfiguration _configuration;
        private string _url;
        private string _webHookUrl;
        private int _timeOut;
        private int _maxTry;

        public PersonBus()
        {
            _configuration = new ConfigurationBuilder()
           .SetBasePath("C:\\Users\\samun\\source\\repos\\Loadtesting\\BusinessLogic")
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();           

            _url = _configuration["PesonApiUrl:BaseUrl"]!;
            _webHookUrl = _configuration["WebHookUrl"]!;
            _timeOut = int.Parse(_configuration["PesonApiUrl:TimeOut"]!);
            _maxTry = int.Parse(_configuration["PesonApiUrl:MaxTry"]!);
        }

        public async Task<bool> GetPersonsAsync(string methodName)
        {
            ClientDTO clientDTO = new ClientDTO(_timeOut, $"{_url}{_maxTry}", RequestType.GET);

            Client client = new Client(clientDTO);

            RequestDto requestDto = new RequestDto(methodName, _maxTry);

            var logs = new ExecuteRequest().ExecuteRequests<List<Person>>(client, requestDto);

            await SeveLogAsync(logs);

            return logs.SuccessRate >= 95;
        }

        //remover dessa camada posteriormente
        public async Task SeveLogAsync(RequestMetricsDto requestMetricsDto)
        {
            string logContent = JsonSerializer.Serialize(requestMetricsDto);

            HttpClient client = new HttpClient();

            var jsonContent = new StringContent(logContent, Encoding.UTF8, "application/json");

            await client.PostAsync(_webHookUrl, jsonContent);
        }
    }
}
