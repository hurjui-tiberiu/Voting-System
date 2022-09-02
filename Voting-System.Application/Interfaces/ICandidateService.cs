using Microsoft.AspNetCore.Mvc;
using Voting_System.Domain.Entities;

namespace Voting_System.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<IActionResult> AddCandidateAsync(Candidate candidate);
        Task<IActionResult> RemoveCandidateAsync(Guid candidateId);
        Task<ActionResult<List<Candidate>>> GetAllCandidatesAsync();
    }
}
