using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Application.Interfaces;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }


        public async Task<IActionResult> AddCandidateAsync(Candidate candidate)
        {
            return await candidateRepository.AddCandidateAsync(candidate);
        }

        public async Task<ActionResult<List<Candidate>>> GetAllCandidatesAsync()
        {
            return await candidateRepository.GetAllCandidatesAsync();
        }

        public async Task<IActionResult> RemoveCandidateAsync(Guid candidateId)
        {
            return await RemoveCandidateAsync(candidateId);
        }
    }
}
