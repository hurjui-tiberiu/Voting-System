using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public async Task AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            var candidate =  mapper.Map<Candidate>(candidateDto);

            await candidateRepository.AddCandidateAsync(candidate);
        }

        public async Task<List<CandidateRequestDto>> GetAllCandidatesAsync()
        {
            var candidates = await candidateRepository.GetAllCandidatesAsync();

            return mapper.Map<List<CandidateRequestDto>>(candidates);
        }

        public async Task RemoveCandidateAsync(Guid candidateId)
        {
            var candidate = await candidateRepository.GetCandidateAsync(candidateId);
            
            if(candidate is not null)
              await candidateRepository.RemoveCandidateAsync(candidate);
        }

        public async Task PatchCandidateAsync(Guid candidateId, dynamic property)
        {
            var candidate = await candidateRepository.GetCandidateAsync(candidateId);
            var candidatePatchDto = JsonConvert.DeserializeObject<CandidatePatchDto>(property.ToString());

            var mappedCandidate = mapper.Map<CandidatePatchDto, Candidate>(candidatePatchDto, candidate);

            await candidateRepository.UpdateCandidateAsync(mappedCandidate);
        }

        public async Task VoteCandidate(Guid candidateId)
        {
            var candidate = await candidateRepository.GetCandidateAsync(candidateId);

            if (candidate is not null)
            {
                candidate.Votes++;
                await candidateRepository.UpdateCandidateAsync(candidate);
            }
        }

    }
}
