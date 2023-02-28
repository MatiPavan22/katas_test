using AutoMapper;
using Common.Errors;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayHandler : IRequestHandler<CreateNewPlayCommand, CreateNewPlayVm>
    {
        private readonly ILogger<CreateNewPlayHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<ErrorData> _errorData;

        public CreateNewPlayHandler(
            ILogger<CreateNewPlayHandler> logger,
            IMapper mapper,
            IOptions<ErrorData> errorData
            )
        {
            _logger = logger;
            _mapper = mapper;
            _errorData = errorData;
        }

        public async Task<CreateNewPlayVm> Handle(CreateNewPlayCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle -> BEGIN");

            _logger.LogInformation("Handler -> Username: {Username}, InitialSquare: {InitialSquare}, SpacesToMoved: {SpacesToMoved} and GameIsStarted: {GameStarted}", request.Username, request.InitialSquare, request.SpacesToMoved, request.GameIsStarted);

            CreateNewPlayVm response = new()
            {
                Username = request.Username
            };

            if (request.GameIsStarted)
            {
                _logger.LogInformation("Handler -> Game is started...");
                response.ResultSquare = request.InitialSquare;
            }
            else
            {
                bool isWon = request.InitialSquare + request.SpacesToMoved == 100;
                int newSquare = request.InitialSquare + request.SpacesToMoved;
                if (newSquare > 100)
                    newSquare = request.InitialSquare;
                _logger.LogInformation("Handler -> Username: {Username} moved to {NewSquare}!!!", request.Username, newSquare);
                if (isWon)
                    _logger.LogInformation("Handler -> Username: {Username} is won!!!", request.Username);

                response.ResultSquare = newSquare;
                response.IsWon = isWon;
            }

            return response;
        }
    }
}
