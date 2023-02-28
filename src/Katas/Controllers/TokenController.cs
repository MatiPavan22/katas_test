using Application.Token.Commands.CreateNewPlay;
using FluentValidation;
using Katas.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static Common.Errors.ErrorData;

namespace Katas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ApiControllerBase
    {
        private readonly IValidator<CreateNewPlayCommand> _validator;

        public TokenController(IValidator<CreateNewPlayCommand> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        [Route("squares")]
        [ProducesResponseType(typeof(CreateNewPlayVm), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateNewPlayVm>> GetNewSquare([FromBody] CreateNewPlayDto body)
        {
            CreateNewPlayCommand command = Mapper.Map<CreateNewPlayCommand>(body);
            var validation = await _validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ErrorDataItem()
                {
                    Description = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var vm = await Mediator.Send(command);

            return new OkObjectResult(vm);
        }
    }
}
