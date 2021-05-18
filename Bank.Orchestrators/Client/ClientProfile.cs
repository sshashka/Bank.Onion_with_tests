using AutoMapper;

namespace Bank.Orchestrators.Client
{
    public class ClientOrchProfile : Profile
    {
        public ClientOrchProfile()
        {
            CreateMap<Clients, Core.Clients.Clients>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
        }
    }
    public class OrchClientProfile : Profile
    {
        public OrchClientProfile()
        {
            CreateMap<Core.Clients.Clients, Clients>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
        }
    }
}