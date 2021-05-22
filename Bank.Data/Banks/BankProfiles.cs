using AutoMapper;

namespace Bank.Data.Banks
{
    public class BankDaoProfile : Profile
    {
        public BankDaoProfile()
        {
            CreateMap<Bank, Core.Banks.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location))
                .ReverseMap();
        }

    }
    /*public class DaoBankProfile : Profile
    {
        public DaoBankProfile()
        {
            CreateMap<Bank, Core.Banks.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.CountOfWorkers))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location))
                .ReverseMap();;
        }

    }*/
}