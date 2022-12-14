using AutoMapper;
using Voting_System.Application.Models.CandidateDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Mapper
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateRequestDto, Candidate>()
                .ForMember(destination => destination.FullName,
            map => map.MapFrom(
                source => source.FullName))
                .ForMember(destination => destination.DateOfBirth,
            map => map.MapFrom(
                source => source.DateOfBirth))
                .ForMember(destination => destination.ShortDescription,
            map => map.MapFrom(
                source => source.ShortDescription))
                  .ForMember(destination => destination.PoliticalParty,
            map => map.MapFrom(
                source => source.PoliticalParty));

            CreateMap<Candidate, CandidateRequestDto>()
                .ForMember(destination => destination.FullName,
            map => map.MapFrom(
                source => source.FullName))
                .ForMember(destination => destination.DateOfBirth,
            map => map.MapFrom(
                source => source.DateOfBirth))
                .ForMember(destination => destination.ShortDescription,
            map => map.MapFrom(
                source => source.ShortDescription))
                  .ForMember(destination => destination.PoliticalParty,
            map => map.MapFrom(
                source => source.PoliticalParty));

            CreateMap<Candidate, CandidatesVotingStatus>()
                .ForMember(destination => destination.Name,
           map => map.MapFrom(
                 source => source.FullName))
                .ForMember(destination => destination.Votes,
           map => map.MapFrom(
                 source => source.Votes));

            CreateMap<CandidatePatchDto, Candidate>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
