using Application.Token.Commands.CreateNewPlay;
using Application.UnitTests.Common;
using AutoFixture;
using AutoMapper;
using Common.Errors;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Token.Commands.CreateNewPlay
{
    [Collection("CreateNewPlayCollection")]
    public class TokenHandlerTests
    {
        private readonly ILogger<CreateNewPlayHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ErrorData _errorData;
        private readonly CreateNewPlayValidator _validator;

        public TokenHandlerTests(CreateNewPlayHandlerFixture fixture)
        {
            #region Configuration

            _logger = fixture._logger;
            _mapper = fixture._mapper;
            _errorData = fixture._errorData;
            _validator = new CreateNewPlayValidator();

            #endregion
        }

        #region US 1

        [Fact]
        public async Task US1_UAT1()
        {
            #region Arrange

            int position_game_started = Constants.SQUARE_GAME_STARTED;
            int spaces_to_move = Constants.US1_UAT1_SPACE_TO_MOVE;
            bool game_started = Constants.GAME_STARTED;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request = new CreateNewPlayCommand()
            {
                GameIsStarted = game_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move
            };

            var result = await sut.Handle(request, CancellationToken.None);

            #endregion

            #region Assert

            result.ShouldBeOfType<CreateNewPlayVm>();
            result.ResultSquare.ShouldBe(position_game_started);
            result.IsWon.ShouldBeFalse();
            result.Username.ShouldBeNull();

            #endregion
        }

        [Fact]
        public async Task US1_UAT2()
        {
            #region Arrange

            int position_game_started = Constants.SQUARE_GAME_STARTED;
            bool game_not_started = Constants.GAME_NOT_STARTED;
            int spaces_to_move = Constants.US1_UAT2_SPACES_MOVE;
            string username = Constants.USERNAME;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request = new CreateNewPlayCommand()
            {
                Username = username,
                GameIsStarted = game_not_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move
            };

            var result = await sut.Handle(request, CancellationToken.None);

            #endregion

            #region Assert

            result.ShouldBeOfType<CreateNewPlayVm>();
            result.ResultSquare.ShouldBe(Constants.US1_UAT2_SQUARE_RESULT);
            result.IsWon.ShouldBeFalse();
            result.Username.ShouldBe(username);

            #endregion
        }

        [Fact]
        public async Task US1_UAT3()
        {
            #region Arrange

            int position_game_started = Constants.SQUARE_GAME_STARTED;
            bool game_not_started = Constants.GAME_NOT_STARTED;
            int spaces_to_move_1 = Constants.US1_UAT3_SPACES_MOVE_1;
            int spaces_to_move_2 = Constants.US1_UAT3_SPACES_MOVE_2;
            string username = Constants.USERNAME;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request_1 = new CreateNewPlayCommand()
            {
                Username = username,
                GameIsStarted = game_not_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move_1
            };

            var result_move_1 = await sut.Handle(request_1, CancellationToken.None);

            var request_2 = new CreateNewPlayCommand()
            {
                Username = username,
                GameIsStarted = game_not_started,
                InitialSquare = result_move_1.ResultSquare,
                SpacesToMoved = spaces_to_move_2
            };

            var result_move_2 = await sut.Handle(request_2, CancellationToken.None);

            #endregion

            #region Assert

            result_move_1.ShouldBeOfType<CreateNewPlayVm>();
            result_move_2.ShouldBeOfType<CreateNewPlayVm>();
            result_move_1.ResultSquare.ShouldBe(Constants.US1_UAT2_SQUARE_RESULT);
            result_move_1.IsWon.ShouldBeFalse();
            result_move_2.IsWon.ShouldBeFalse();
            result_move_1.Username.ShouldBe(username);
            result_move_2.Username.ShouldBe(username);
            result_move_2.ResultSquare.ShouldBe(Constants.US1_UAT3_SQUARE_RESULT);

            #endregion
        }

        #endregion

        #region US 2

        [Fact]
        public async Task US2_UAT1()
        {
            #region Arrange

            int position_game_started = Constants.US2_UAT1_SQUARE_INIT;
            int spaces_to_move = Constants.US2_UAT1_SPACES_MOVE;
            bool game_started = Constants.GAME_NOT_STARTED;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request = new CreateNewPlayCommand()
            {
                GameIsStarted = game_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move,
                Username = Constants.USERNAME
            };

            var result = await sut.Handle(request, CancellationToken.None);

            #endregion

            #region Assert

            result.ShouldBeOfType<CreateNewPlayVm>();
            result.ResultSquare.ShouldBe(Constants.US2_UAT1_SQUARE_RESULT);
            result.IsWon.ShouldBeTrue();
            result.Username.ShouldBe(Constants.USERNAME);

            #endregion
        }

        [Fact]
        public async Task US2_UAT2()
        {
            #region Arrange

            int position_game_started = Constants.US2_UAT2_SQUARE_INIT;
            int spaces_to_move = Constants.US2_UAT2_SPACES_MOVE;
            bool game_started = Constants.GAME_NOT_STARTED;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request = new CreateNewPlayCommand()
            {
                GameIsStarted = game_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move,
                Username = Constants.USERNAME
            };

            var result = await sut.Handle(request, CancellationToken.None);

            #endregion

            #region Assert

            result.ShouldBeOfType<CreateNewPlayVm>();
            result.ResultSquare.ShouldBe(Constants.US2_UAT2_SQUARE_INIT);
            result.IsWon.ShouldBeFalse();
            result.Username.ShouldBe(Constants.USERNAME);

            #endregion
        }

        #endregion

        #region US 3

        [Fact]
        public async Task US3_UAT1_OK_1Moves()
        {
            #region Arrange

            Fixture autofixture = new();

            var command = autofixture.Build<CreateNewPlayCommand>()
                .With(x => x.SpacesToMoved, 1)
                .With(x => x.GameIsStarted, value: false)
                .Create();

            #endregion

            #region Act

            var result = _validator.TestValidate(command);

            #endregion

            #region Assert

            result.ShouldNotHaveValidationErrorFor(x => x.SpacesToMoved);

            #endregion
        }

        [Fact]
        public async Task US3_UAT1_OK_6Moves()
        {
            #region Arrange

            Fixture autofixture = new();

            var command = autofixture.Build<CreateNewPlayCommand>()
                .With(x => x.SpacesToMoved, 6)
                .With(x => x.GameIsStarted, value: false)
                .Create();

            #endregion

            #region Act

            var result = _validator.TestValidate(command);

            #endregion

            #region Assert

            result.ShouldNotHaveValidationErrorFor(x => x.SpacesToMoved);

            #endregion
        }

        [Fact]
        public async Task US3_UAT1_KO_7Moves()
        {
            #region Arrange

            Fixture autofixture = new();

            var command = autofixture.Build<CreateNewPlayCommand>()
                .With(x => x.SpacesToMoved, 7)
                .Create();

            #endregion

            #region Act

            var result = _validator.TestValidate(command);

            #endregion

            #region Assert

            result.ShouldHaveValidationErrorFor(x => x.SpacesToMoved);

            #endregion
        }

        [Fact]
        public async Task US3_UAT2()
        {
            #region Arrange

            int position_game_started = Constants.US3_UAT2_SQUARE_INIT;
            int spaces_to_move = Constants.US3_UAT2_SPACES_MOVE;
            bool game_started = Constants.GAME_NOT_STARTED;

            #endregion

            #region Act

            var sut = new CreateNewPlayHandler(_logger, _mapper, Options.Create(_errorData));

            var request = new CreateNewPlayCommand()
            {
                GameIsStarted = game_started,
                InitialSquare = position_game_started,
                SpacesToMoved = spaces_to_move,
                Username = Constants.USERNAME
            };

            var result = await sut.Handle(request, CancellationToken.None);

            #endregion

            #region Assert

            result.ShouldBeOfType<CreateNewPlayVm>();
            result.ResultSquare.ShouldBe(Constants.US3_UAT2_SQUARE_RESULT);
            result.IsWon.ShouldBeFalse();
            result.Username.ShouldBe(Constants.USERNAME);

            #endregion
        }

        #endregion


    }
}
