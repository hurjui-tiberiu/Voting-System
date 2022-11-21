using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Voting_System.Application.Interfaces;
using Voting_System.Application.JWTUtil;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Extensions;

namespace Voting_System.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]/")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService candidateService;
        private readonly ILogger logger;
        private readonly IValidator<CandidateRequestDto> candidateRequestDtoValidator;
        private readonly IValidator<CandidatePatchDto> candidatePatchDtoValidator;
        private readonly IDistributedCache cache;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger,
        IValidator<CandidateRequestDto> candidateRequestDtoValidator, IValidator<CandidatePatchDto> candidatePatchDtoValidator,
        IDistributedCache cache)
        {
            this.candidateService = candidateService;
            this.logger = logger;
            this.candidateRequestDtoValidator = candidateRequestDtoValidator;
            this.candidatePatchDtoValidator = candidatePatchDtoValidator;
            this.cache = cache;
        }

        [SwaggerOperation(Summary = "Get all candidates | Auth:Anonymous")]
        [HttpGet, Route("candidates"), AllowAnonymous]
        public async Task<ActionResult<List<CandidateRequestDto>>> GetAllCandidatesAsync()
        {
            try
            {
                string recordKey = "Candidates_" + DateTime.Now.ToString();

                var candidates = await cache.GetRecordAsync<List<CandidateRequestDto>>(recordKey);

                if (candidates is null)
                {
                    candidates = await candidateService.GetAllCandidatesAsync();
                    await cache.SetRecordAsync<List<CandidateRequestDto>>(recordKey, candidates);

                }

                logger.LogInformation("Candidates retrived: {count}", candidates.Count);

                return Ok(candidates);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
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

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Add candidate | Auth:Admin")]
        [HttpPut, Route("addcandidate"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<IActionResult> AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            try
            {
                var result = await candidateRequestDtoValidator.ValidateAsync(candidateDto);

                if (!result.IsValid)
                    return BadRequest(result.ToString());

                await candidateService.AddCandidateAsync(candidateDto);

                logger.LogInformation("Candidate created succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Edit candidate | Auth:Admin")]
        [HttpPatch, Route("{candidateId}"), AuthorizeMultiplePolicy(Policies.Admin, true)]
        public async Task<IActionResult> PatchCandidateAsync(Guid candidateId, dynamic property)
        {
            try
            {
                var candidatePatchDto = JsonConvert.DeserializeObject<CandidatePatchDto>(property.ToString());

                var result = await candidatePatchDtoValidator.ValidateAsync(candidatePatchDto);

                if (!result.IsValid)
                    return BadRequest(result.ToString());

                await candidateService.PatchCandidateAsync(candidateId, candidatePatchDto);

                logger.LogInformation("Candidate updated succesfully");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
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

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Check voting status | Auth:User")]
        [HttpGet, Route("votingstatus"), AuthorizeMultiplePolicy(Policies.User + ";" + Policies.Admin, false)]
        public async Task<ActionResult<List<CandidatesVotingStatus>>> GetCandidatesVotingStatusAsync()
        {
            try
            {
                string recordKey = "CandidatesVotingStatus_" + DateTime.Now.ToString();

                var candidatesVotingStatus = await cache.GetRecordAsync<List<CandidatesVotingStatus>>(recordKey);

                if (candidatesVotingStatus is null)
                {
                    candidatesVotingStatus = await candidateService.GetCandidatesVotingStatusAsync();
                    await cache.SetRecordAsync<List<CandidatesVotingStatus>>(recordKey, candidatesVotingStatus);

                }

                return Ok(candidatesVotingStatus);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}