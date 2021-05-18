using AutoMapper;

namespace Bank.Orchestrators.Bank
{
    public class BankOrchProfile : Profile
    {
        public BankOrchProfile()
        {
            CreateMap<Bank, Core.Bank.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.CountOfWorkers, memberOptions: opt => opt.MapFrom(src => src.CountOfWorkers))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location));
        }

    }
    public class OrchBankProfile : Profile
    {
        public OrchBankProfile()
        {
            CreateMap<Bank, Core.Bank.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.CountOfWorkers, memberOptions: opt => opt.MapFrom(src => src.CountOfWorkers))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location));
        }

    }
}