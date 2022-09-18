using FluentValidation;
using System.Text.RegularExpressions;
using Voting_System.Application.Models.UserDto;

namespace Voting_System.Application.Models.FluentValidation.UserValidators
{
    public class UserPatchDtoValidator : AbstractValidator<UserPatchDto>
    {
        public UserPatchDtoValidator()
        {
            RuleFor(entity => entity.FullName)
                .MinimumLength(5).WithMessage("Your full name can not be empty")
                .Matches(new Regex(
                @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
                .WithMessage("Bad full name format");

            RuleFor(entity => entity.IdentityCardId)
                .Matches(new Regex(@"^(\d{13})?$")).WithMessage("Invalid personal ID");

            RuleFor(entity => Convert.ToInt32(entity.IdentityCardNumber))
               .InclusiveBetween(100000, 999999).WithMessage("Invalid personal number");

            RuleFor(entity => entity.IdentityCardSeries)
                .Matches(new Regex(@"^[A-Za-z]{2}\z")).WithMessage("Invalid personal series");

            RuleFor(entity => entity.IdentityCardEmitedDate)
                .LessThan(DateTime.Now)
                .GreaterThan(DateTime.Now.AddYears(-7))
                .WithMessage("Invalid emited date");

            RuleFor(entity => entity.Mail)
                .EmailAddress().WithMessage("Invalid email address");
        }
    }
}
