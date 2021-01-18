using Microsoft.AspNetCore.Mvc;
using Recruitment.API.Services;
using Recruitment.API.Validation;
using Recruitment.Contracts;
using System.Threading.Tasks;

namespace Recruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HashController : ControllerBase
    {
        private readonly IHashService _hashService;

        public HashController(IHashService hashService)
        {
            _hashService = hashService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ContractModel contractModel)
        {
            var validator = new ContractModelValidator();
            if (!validator.Validate(contractModel))
            {
                return new BadRequestResult();
            }

            await _hashService.CreateHashAsync(contractModel);
            return Ok();
        }
    }
}
