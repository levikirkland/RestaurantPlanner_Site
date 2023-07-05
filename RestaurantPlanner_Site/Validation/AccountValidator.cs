using FluentValidation;
using RestaurantPlanner_Site.Interfaces;
using RestaurantPlanner_Site.Models;
using RestaurantPlanner_Site.Services;

namespace RestaurantPlanner_Site.Validation
{
    public class AccountInfoValidator : AbstractValidator<AccountInfo>
    {
        private readonly ISignUpConfig _signUpConfig;

        public AccountInfoValidator(ISignUpConfig signUpConfig)
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull();
            RuleFor(p => p.LastName).NotEmpty().NotNull();
            RuleFor(p => p.Address1).NotEmpty().NotNull();
            RuleFor(p => p.EmailAddress).NotEmpty().EmailAddress().WithMessage("We suggest only using a company email address here.")
                .MustAsync(BeUniqueEmailAddress).WithMessage("Email Address is already in use.");
            RuleFor(p => p.City).NotEmpty().WithMessage("Valid City within your State is necessary.");
            RuleFor(p => p.State).NotNull().NotEmpty().WithMessage("Please select State");
            RuleFor(p => p.Zipcode).NotEmpty().WithMessage("Please add zip code.").MaximumLength(5);
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Please add phone number").MaximumLength(10);
            _signUpConfig = signUpConfig;
        }

        public async Task<bool> BeUniqueEmailAddress(AccountInfo accountInfo, string EmailAddress, CancellationToken cancellationToken)
        {
            return await new AccountService(_signUpConfig).IsUniqueEmail(EmailAddress);
        }
    }
}
