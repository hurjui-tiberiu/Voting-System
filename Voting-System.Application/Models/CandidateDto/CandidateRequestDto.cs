using Voting_System.Domain.Enums;

namespace Voting_System.Application.Models.CandidateDto
{
    public record CandidateRequestDto
    {
        public string? FullName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public Party PoliticalParty { set; get; }
        public string? ShortDescription { set; get; }
    }
}
