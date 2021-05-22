using System;
using AutoMapper;

namespace Bank.Orchestrators.Banks
{
    public class BankOrchProfile : Profile
    {
        public BankOrchProfile()
        {
            CreateMap<Bank, Core.Banks.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location))
                .ReverseMap();;
        }

    }
    public class OrchBankProfile : Profile
    {
        public OrchBankProfile()
        {
            CreateMap<Bank, Core.Banks.Bank>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
                .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location))
                .ReverseMap();;
        }

    }
}
