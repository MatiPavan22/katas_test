using Application.Token.Commands.CreateNewPlay;
using Application.UnitTests.Common;
using AutoMapper;
using Common.Errors;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Application.UnitTests.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayHandlerFixture
    {
        public ILogger<CreateNewPlayHandler> _logger;
        public IMapper _mapper;
        public ErrorData _errorData;

        public CreateNewPlayHandlerFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _logger = Common.LoggerFactory.Create<CreateNewPlayHandler>();
            _errorData = ErrorDataFactory.Create();

        }
    }

    [CollectionDefinition("CreateNewPlayCollection")]
    public class CreateNewPlayCollection : ICollectionFixture<CreateNewPlayHandlerFixture> { }
}
