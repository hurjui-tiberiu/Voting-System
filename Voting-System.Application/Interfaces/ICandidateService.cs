using Voting_System.Application.Models.CandidateDto;

namespace Voting_System.Application.Interfaces
{
    public interface ICandidateService
    {
        Task AddCandidateAsync(CandidateRequestDto candidate);
        Task RemoveCandidateAsync(Guid candidateId);
        Task<List<CandidateRequestDto>> GetAllCandidatesAsync();
        Task PatchCandidateAsync(Guid candidateId, dynamic property);
        Task<bool> VoteCandidateAsync(Guid userId, Guid candidateId);
        Task<List<CandidatesVotingStatus>> GetCandidatesVotingStatusAsync();
    }
}
