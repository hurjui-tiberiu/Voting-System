using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<IActionResult> AddCandidateAsync(CandidateRequestDto candidate);
        Task<IActionResult> RemoveCandidateAsync(Guid candidateId);
        Task<ActionResult<List<CandidateRequestDto>>> GetAllCandidatesAsync();
    }
}
