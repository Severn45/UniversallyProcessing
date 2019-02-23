using AutoMapper;

namespace UniversallyProcessing.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, UserDto>().ForMember(u => u.Provider, opt => opt.MapFrom(u => u.LoginProvider)).ReverseMap();
            //CreateMap<User, CredentialsDto>().ForMember(c => c.Name, opt => opt.MapFrom(u => u.UserName)).ReverseMap();
            //CreateMap<WorkTask, TaskDto>().ReverseMap();
            //CreateMap<ExchangeRate, ExchangeRateDto>().ForMember(dto => dto.Rate, opt => opt.MapFrom(rate => Math.Round(rate.Rate, 2))).ReverseMap();
        }
    }
}
