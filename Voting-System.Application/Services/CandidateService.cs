using AutoMapper;
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
        private readonly IUserRepository userRepository;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.candidateRepository = candidateRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task AddCandidateAsync(CandidateRequestDto candidateDto)
        {
            var candidate = mapper.Map<Candidate>(candidateDto);

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

            if (candidate is not null)
                await candidateRepository.RemoveCandidateAsync(candidate);
        }

        public async Task PatchCandidateAsync(Guid candidateId, CandidatePatchDto candidatePatchDto)
        {
            var candidate = await candidateRepository.GetCandidateAsync(candidateId);

            if (candidate is not null)
            {
                var mappedCandidate = mapper.Map<CandidatePatchDto, Candidate>(candidatePatchDto, candidate);

                await candidateRepository.UpdateCandidateAsync(mappedCandidate);
            }
        }

        public async Task<bool> VoteCandidateAsync(Guid userId, Guid candidateId)
        {
            var user = await userRepository.GetUserByPropertyAsync(x => x.Id == userId);

            if (user is null || user.Voted == true)
                return false;

            var candidate = await candidateRepository.GetCandidateAsync(candidateId);

            if (candidate is not null)
            {
                candidate.Votes++;
                await candidateRepository.UpdateCandidateAsync(candidate);
                user.Voted = true;
                await userRepository.UpdateUserAsync(user);
            }

            return true;
        }

        public async Task<List<CandidatesVotingStatus>> GetCandidatesVotingStatusAsync()
        {
            var candidates = await candidateRepository.GetAllCandidatesAsync();

            var votesCount = candidates.Sum(candidate => candidate.Votes);

            var candidateVotingStatus = mapper.Map<List<CandidatesVotingStatus>>(candidates);

            foreach (var candidateDto in candidateVotingStatus)
            {
                if (candidateDto.Votes > 0)
                    candidateDto.PercentageOfVotes = (Convert.ToDecimal(candidateDto.Votes) / votesCount) * 100;
                else candidateDto.PercentageOfVotes = 0;
            }

            return candidateVotingStatus;
        }
    }
}
