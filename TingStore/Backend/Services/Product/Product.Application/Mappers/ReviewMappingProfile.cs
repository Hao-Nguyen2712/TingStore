using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Product.Application.Commands;
using Product.Application.Responses;
using Product.Core.Models;

namespace Product.Application.Mappers
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, ReviewResponse>().ReverseMap();
            CreateMap<Review, AddReviewCommand>().ReverseMap();
        }
    }
}
