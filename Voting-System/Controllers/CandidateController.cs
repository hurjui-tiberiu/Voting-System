﻿

using Microsoft.AspNetCore.Mvc;
using Voting_System.Application.Interfaces;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Controllers
{
    [Route("api/[controler]"), ApiController]
    public class CandidateController:ControllerBase
    {
        private readonly ICandidateService candidateService;
        private readonly ILogger<CandidateController> logger;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
        {
            this.candidateService = candidateService;
            this.logger = logger;
        }

        [HttpGet, Route("/get")]
        public async Task<ActionResult<List<CandidateRequestDto>>> GetAllCandidatesAsync()
        {
            return await candidateService.GetAllCandidatesAsync();
        }

        [HttpDelete, Route("/delete")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid candidateId)
        {
            return await candidateService.RemoveCandidateAsync(candidateId);
        }

        [HttpPut, Route("/put")]
        public async Task<IActionResult> AddCandidate(CandidateRequestDto candidateDto)
        {
            return await candidateService.AddCandidateAsync(candidateDto);
        }
        
    }
}