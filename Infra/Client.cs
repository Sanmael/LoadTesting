using System.Text;
using System.Text.Json;


namespace Infra
{
    public class Client
    {
        private HttpClient _client;
        private ClientDTO _clientDto;

        public Client(ClientDTO clientDTO)
        {
            _clientDto = clientDTO;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(clientDTO.Uri);
            _client.Timeout = TimeSpan.FromSeconds(clientDTO.TimeOut);
        }

        public Task<HttpResponseMessage> SendRequest()
        {
            switch (_clientDto.RequestType)
            {
                case RequestType.GET:
                    return SendGetRequestAsync();
                case RequestType.POST:
                    return SendPostRequestAsync();

                default: throw new NotImplementedException();
            }
        }

        private async Task<HttpResponseMessage> SendPostRequestAsync()
        {
            var jsonContent = JsonSerializer.Serialize(_clientDto.Entity);
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");            
            return await _client.PostAsync(_client.BaseAddress, content);
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync()
        {
            return await _client.GetAsync(_client.BaseAddress);
        }
    }
}
