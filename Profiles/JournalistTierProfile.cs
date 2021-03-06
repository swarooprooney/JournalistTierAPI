using AutoMapper;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Profiles
{
    public class JournalistTierProfile : Profile
    {
        public JournalistTierProfile()
        {
            CreateMap<Journalist, JournalistDto>();
            CreateMap<Topic, TopicDto>();
            CreateMap<Media, MediaDto>();
            CreateMap<UserJournalistRatingDto, UserJournalistRating>();
            CreateMap<UserMediaRatingDto, UserMediaRating>();
            CreateMap<TierQueryDto, UserJournalistRating>();
            CreateMap<TierQueryDto, UserMediaRating>();
            CreateMap<User, UserDto>();
            CreateMap<UserForUpdateDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<MediaDto, Media>();
            CreateMap<JournalistTierQueryDto, TierQueryDto>();
            CreateMap<MediaTierQueryDto, TierQueryDto>();
        }
    }
}