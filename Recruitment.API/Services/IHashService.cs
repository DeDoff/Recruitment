using Recruitment.Contracts;
using System.Threading.Tasks;

namespace Recruitment.API.Services
{
    public interface IHashService
    {
        Task<string> CreateHashAsync(ContractModel contractModel);
    }
}
