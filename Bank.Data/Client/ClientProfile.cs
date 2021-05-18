using AutoMapper;

namespace Bank.Data.Client
{
    public class ClientDaoProfile : Profile
    {
        public ClientDaoProfile()
        {
            CreateMap<Clients, Core.Clients.Clients>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
        }
    }
    public class DaoClientProfile : Profile
    {
        public DaoClientProfile()
        {
            CreateMap<Core.Clients.Clients, Clients>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
        }
    }
}