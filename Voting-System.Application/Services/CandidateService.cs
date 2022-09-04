using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Domain.Entities;
using Voting_System.Infrastructure.Interfaces;

namespace Voting_System.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IMapper mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            this.candidateRepository = candidateRepository;
            this.mapper = mapper;
        }


        public async Task<IActionResult> AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            var candidate =  mapper.Map<Candidate>(candidateDto);

            return await candidateRepository.AddCandidateAsync(candidate);
        }

        public async Task<ActionResult<List<CandidateRequestDto>>> GetAllCandidatesAsync()
        {
            var candidates = await candidateRepository.GetAllCandidatesAsync();

            return mapper.Map<List<CandidateRequestDto>>(candidates);
        }

        public async Task<IActionResult> RemoveCandidateAsync(Guid candidateId)
        {
            return await RemoveCandidateAsync(candidateId);
        }
    }
}
