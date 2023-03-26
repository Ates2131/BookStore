using FluentValidation;

namespace WebApi.BookOperations.DeleteBooks
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.BookId).GreaterThanOrEqualTo(1);
        }
    }
}