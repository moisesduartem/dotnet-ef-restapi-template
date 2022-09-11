using AutoMapper;
using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Infra.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoggedUserDTO>();

            CreateMap<User, LoginDTO>()
                .ForMember(d => d.User, cfg => cfg.MapFrom(x => x))
                .ForMember(d => d.Token, cfg => cfg.Ignore());

            CreateMap<string, LoginDTO>()
                .ForMember(
                    d => d.User,
                    cfg => cfg.Ignore()
                )
                .ForMember(
                    d => d.Token,
                    cfg => cfg.MapFrom(x => x)
                );
        }
    }
}
