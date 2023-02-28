using FluentValidation;

namespace Application.Token.Commands.CreateNewPlay
{
    public class CreateNewPlayValidator : AbstractValidator<CreateNewPlayCommand>
    {
        public CreateNewPlayValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .When(x => !x.GameIsStarted && x.InitialSquare >= 1 && x.SpacesToMoved >= 1);

            RuleFor(x => x.InitialSquare)
                .GreaterThanOrEqualTo(1)
                .When(x => !x.GameIsStarted);

            RuleFor(x => x.SpacesToMoved)
                .Equal(0)
                .When(x => x.GameIsStarted);

            RuleFor(x => x.InitialSquare)
                .Equal(1)
                .When(x => x.GameIsStarted);

            RuleFor(x => x.SpacesToMoved)
                .LessThanOrEqualTo(6)
                .GreaterThanOrEqualTo(1)
                .When(x => !x.GameIsStarted && !string.IsNullOrEmpty(x.Username));
        }
    }
}
