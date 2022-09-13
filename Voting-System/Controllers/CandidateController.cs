

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
            var candidates = await candidateService.GetAllCandidatesAsync();

            return Ok(candidates);
        }

        [HttpDelete, Route("/delete")]
        public async Task<IActionResult> DeleteCandidateAsync(Guid candidateId)
        {
           await candidateService.RemoveCandidateAsync(candidateId);

            return Ok();
        }

        [HttpPut, Route("/put")]
        public async Task<IActionResult> AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            await candidateService.AddCandidateAsync(candidateDto);

            return Ok();
        }

        [HttpPatch, Route("/patch")]
        public async Task<IActionResult> PatchCandidateAsync(Guid candidateId, dynamic property)
        {
            await candidateService.PatchCandidateAsync(candidateId, property);

            return Ok();
        }

        [HttpPost, Route("/vote")]
        public async Task<IActionResult> VoteCandidateAsync(Guid candidateId)
        {
            await candidateService.VoteCandidate(candidateId);

            return Ok();
        }
    }
}