using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Application.Interfaces
{
    public interface ICandidateService
    {
        Task AddCandidateAsync(CandidateRequestDto candidate);
        Task RemoveCandidateAsync(Guid candidateId);
        Task<List<CandidateRequestDto>> GetAllCandidatesAsync();
        Task PatchCandidateAsync(Guid candidateId, dynamic property);
        Task VoteCandidate(Guid candidateId);
    }
}
