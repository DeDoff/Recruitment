using Recruitment.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recruitment.API.Services
{
    public class HashService : IHashService
    {
        public async Task<string> CreateHashAsync(ContractModel contractModel)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", contractModel.Login),
                new KeyValuePair<string, string>("password", contractModel.Password)
            });
            var response = await client.PostAsync("http://localhost:7071/api/HashingFunction", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
