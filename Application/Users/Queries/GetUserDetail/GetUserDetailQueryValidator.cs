using FluentValidation;

namespace Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.UserId)
                .NotEqual(0);
        }
    }
}
