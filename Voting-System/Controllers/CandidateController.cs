using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Voting_System.Application.Interfaces;
using Voting_System.Application.JWTUtil;
using Voting_System.Application.Models.CandidateDto;

namespace Voting_System.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]/")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService candidateService;
        private readonly ILogger logger;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
        {
            this.candidateService = candidateService;
            this.logger = logger;
        }

        [SwaggerOperation(Summary = "Get all candidates | Auth:Anonymous")]
        [HttpGet, Route("candidates"), AllowAnonymous]
        public async Task<ActionResult<List<CandidateRequestDto>>> GetAllCandidatesAsync()
        {
            try
            {
                var candidates = await candidateService.GetAllCandidatesAsync();

                logger.LogInformation("Candidates retrived: {count}", candidates.Count);

                return Ok(candidates);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Delete candidate | Auth:Admin")]
        [HttpDelete, Route("{candidateId}"), AuthorizeMultiplePolicy(Policies.Admin, false)]
        public async Task<IActionResult> DeleteCandidateAsync(Guid candidateId)
        {
            try
            {
                await candidateService.RemoveCandidateAsync(candidateId);

                logger.LogInformation("Candidate deleted succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Add candidate | Auth:Admin")]
        [HttpPut, Route("addcandidate"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<IActionResult> AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            try
            {
                await candidateService.AddCandidateAsync(candidateDto);

                logger.LogInformation("Candidate created succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Edit candidate | Auth:Admin")]
        [HttpPatch, Route("{candidateId}"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<IActionResult> PatchCandidateAsync(Guid candidateId, dynamic property)
        {
            try
            {
                await candidateService.PatchCandidateAsync(candidateId, property);

                logger.LogInformation("Candidate updated succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Vote candidate | Auth:User")]
        [HttpPost, Route("vote/{userId}/{candidateId}"), AuthorizeMultiplePolicy(Policies.User, true)]
        public async Task<IActionResult> VoteCandidateAsync(Guid userId, Guid candidateId)
        {
            try
            {
                var result = await candidateService.VoteCandidateAsync(userId, candidateId);

                if (result == false)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }

        [SwaggerOperation(Summary = "Check voting status | Auth:User")]
        [HttpGet, Route("votingstatus"), AuthorizeMultiplePolicy(Policies.User + ";" + Policies.Admin, false)]
        public async Task<ActionResult<List<CandidatesVotingStatus>>> GetCandidatesVotingStatusAsync()
        {
            try
            {
                var candidatesVotingStatus = await candidateService.GetCandidatesVotingStatusAsync();

                return Ok(candidatesVotingStatus);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest();
            }
        }
    }
}