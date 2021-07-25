using FluentValidation;

namespace HPC.Application.Queries
{
    public abstract class ListQueryBase
    {
        public int Page { get; }
        public int Size { get; }

        protected ListQueryBase(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }

    public class ListQueryBaseValidator : AbstractValidator<ListQueryBase>
    {
        public ListQueryBaseValidator()
        {
            RuleFor(query => query)
                .NotEmpty();

            RuleFor(query => query.Page)
                .GreaterThan(0);

            RuleFor(query => query.Size)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);
        }
    }
}
