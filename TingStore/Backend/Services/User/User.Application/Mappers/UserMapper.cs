// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace User.Application.Mappers
{
    public static class UserMapper
    {
        // Sử dụng Lazy<IMapper> để đảm bảo Mapper chỉ khởi tạo khi cần
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            // Cấu hình AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                // Chỉ ánh xạ những thuộc tính có phương thức Get là Public hoặc Internal (tránh ánh xạ các property private)
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                // Nạp cấu hình ánh xạ từ UserMappingProfile (chứa quy tắc chuyển đổi giữa Entity và DTO)
                cfg.AddProfile<UserMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    } 
}
