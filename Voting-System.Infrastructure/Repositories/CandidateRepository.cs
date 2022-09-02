using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        public Task<IActionResult> AddCandidateAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<Candidate>>> GetAllCandidatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> RemoveCandidateAsync(Guid candidateId)
        {
            throw new NotFiniteNumberException();
        }
    }
}
