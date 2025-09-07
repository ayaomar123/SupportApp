using FluentValidation;

namespace SupportApp.Application.Features.Tickets.Commands.CreateTicketActivity
{
    public class CreateTicketActivityCommandValidator : AbstractValidator<CreateTicketActivityCommand>
    {
        public CreateTicketActivityCommandValidator()
        {
            RuleFor(x => x.TicketId).NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            When(x => x.NewStatus.HasValue, () =>
            {
                RuleFor(x => x.NewStatus!.Value).IsInEnum();
            });
        }
    }
}
