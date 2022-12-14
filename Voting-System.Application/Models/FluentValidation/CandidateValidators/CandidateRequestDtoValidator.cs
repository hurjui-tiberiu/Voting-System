using FluentValidation;
using System.Text.RegularExpressions;
using Voting_System.Application.Models.CandidateDto;

namespace Voting_System.Application.Models.FluentValidation.CandidateValidators
{
    public class CandidateRequestDtoValidator : AbstractValidator<CandidateRequestDto>
    {
        public CandidateRequestDtoValidator()
        {

            RuleFor(entity => entity.FullName)
                .MinimumLength(5).WithMessage("Your full name can not be empty")
                .Matches(new Regex(
                @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
                .WithMessage("Bad full name format");

            RuleFor(entity => entity.DateOfBirth)
                .LessThan(DateTime.Now)
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-18))
                .WithMessage("Invalid date of birth");

            RuleFor(entity => entity.ShortDescription)
                .MinimumLength(20).WithMessage("Description is too short")
                .MaximumLength(100).WithMessage("Description is too long");
        }
    }
}
