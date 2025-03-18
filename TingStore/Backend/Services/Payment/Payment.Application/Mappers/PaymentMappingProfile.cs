using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Payment.Application.Dtos;
using Payment.Core.Entities;

namespace Payment.Application.Mappers
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<PaymentTransaction, PaymentTransactionDTO>().ReverseMap();
            CreateMap<PaymentTransactionDTO, PaymentTransaction>().ReverseMap();
            CreateMap<Commands.AddTransactionCommand, PaymentTransaction>().ReverseMap();
        }
    }
}
