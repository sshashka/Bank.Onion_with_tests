﻿using System;
using AutoMapper;

namespace Bank.Orchestrators.Clients
{
    public class ClientOrchProfile : Profile
    {
        public ClientOrchProfile()
        {
            CreateMap<Client, Core.Clients.Clients>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum))
                .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.BankId))
                .ReverseMap();
        }
    }
}
