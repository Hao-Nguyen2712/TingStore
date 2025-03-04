using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Cart.Application.Mappers
{
    public static class CartMapper
    {
        // implement a lazy loading singleton pattern for the mapper
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CartMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;  
        });

        public static IMapper Mapper => Lazy.Value;

    }
}
