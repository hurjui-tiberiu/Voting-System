using Voting_System.Domain.Entities;

namespace Voting_System.Infrastructure.Interfaces
{
    public interface ICandidateRepository
    {
        Task AddCandidateAsync(Candidate candidate);
        Task RemoveCandidateAsync(Candidate candidate);
        Task<List<Candidate>> GetAllCandidatesAsync();
        Task<Candidate?> GetCandidateAsync(Guid candidateId);
        Task UpdateCandidateAsync(Candidate candidate);
    }
}
