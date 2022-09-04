using AutoMapper;
using Voting_System.Application.Models.UserDto;
using Voting_System.Domain.Entities;

namespace Voting_System.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDto, User>()
                .ForMember(destination => destination.FullName,
            map => map.MapFrom(
                source => source.FullName))
                .ForMember(destination => destination.IdentityCardNumber,
            map => map.MapFrom(
                source => source.IdentityCardNumber))
                .ForMember(destination => destination.IdentityCardId,
            map => map.MapFrom(
                source => source.IdentityCardId))
                .ForMember(destination => destination.IdentityCardSeries,
            map => map.MapFrom(
                source => source.IdentityCardSeries))
                .ForMember(destination => destination.IdentityCardEmitedDate,
            map => map.MapFrom(
                source => source.IdentityCardEmitedDate));
        }
    }
}
