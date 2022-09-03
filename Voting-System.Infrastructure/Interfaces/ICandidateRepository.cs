using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Domain.Entities;

namespace Voting_System.Infrastructure.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IActionResult> AddCandidateAsync(Candidate candidate);
        Task<IActionResult> RemoveCandidateAsync(Guid candidateId);
        Task<ActionResult<List<Candidate>>> GetAllCandidatesAsync();
    }
}
