using System.Net.Http;
using System.Threading.Tasks;

namespace ReceitaWSAPI.Services
{
    public class ReceitaWsService
    {
        private readonly HttpClient _httpClient;

        public ReceitaWsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCompanyDataAsync(string cnpj)
        {
            var response = await _httpClient.GetAsync($"https://receitaws.com.br/v1/cnpj/{cnpj}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erro ao solicitar dados da ReceitaWS");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
