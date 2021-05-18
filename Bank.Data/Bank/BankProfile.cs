using AutoMapper;

namespace Bank.Data.Bank
{
    public class BankDaoProfile : Profile
    {
        public BankDaoProfile()
        {
            CreateMap<Bank, Core.Bank.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.CountOfWorkers, memberOptions: opt => opt.MapFrom(src => src.CountOfWorkers))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location));
        }

    }
    public class DaoBankProfile : Profile
    {
        public DaoBankProfile()
        {
            CreateMap<Bank, Core.Bank.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.CountOfWorkers, memberOptions: opt => opt.MapFrom(src => src.CountOfWorkers))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location));
        }

    }
}