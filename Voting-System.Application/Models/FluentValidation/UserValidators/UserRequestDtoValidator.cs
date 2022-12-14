using FluentValidation;
using System.Text.RegularExpressions;
using Voting_System.Application.Models.UserDto;

namespace Voting_System.Application.Models.FluentValidation.UserValidators
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator()
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

            RuleFor(entity => entity.Password)
                .Matches(new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
                .WithMessage("Password must be at least 8 characters long, must contain at least one: " +
                "upper case character, lower case character," +
                 " digit character, special character");
        }
    }
}
