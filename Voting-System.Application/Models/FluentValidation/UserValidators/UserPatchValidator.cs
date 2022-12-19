using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Voting_System.Application.Models.UserDto;

namespace Voting_System.Application.Models.FluentValidation.UserValidators
{
    public class UserPatchValidator:AbstractValidator<UserPatchDto>
    {
        public UserPatchValidator()
        {
            RuleFor(entity => entity.FullName)
               .MinimumLength(5).WithMessage("Your full name can not be empty")
               .Matches(new Regex(
               @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
               .When(entity => entity.FullName != null)
               .WithMessage("Bad full name format");

            RuleFor(entity => entity.IdentityCardId)
                .Matches(new Regex(@"^(\d{13})?$"))
                .When(entity => entity.IdentityCardId != null)
                .WithMessage("Invalid personal ID");

            RuleFor(entity => Convert.ToInt32(entity.IdentityCardNumber))
               .InclusiveBetween(100000, 999999)
               .When(entity => entity.IdentityCardNumber != null)
               .WithMessage("Invalid personal number");

            RuleFor(entity => entity.IdentityCardSeries)
                .Matches(new Regex(@"^[A-Za-z]{2}\z"))
                .When(entity => entity.IdentityCardSeries != null) 
                .WithMessage("Invalid personal series");

            RuleFor(entity => entity.IdentityCardEmitedDate)
                .LessThan(DateTime.Now)
                .GreaterThan(DateTime.Now.AddYears(-7))
               .When(entity => entity.IdentityCardEmitedDate != null)
                               .WithMessage("Invalid emited date");

            RuleFor(entity => entity.Mail)
                .EmailAddress().WithMessage("Invalid email address").
                When(entity => entity.Mail != null);
        }
    }
}
