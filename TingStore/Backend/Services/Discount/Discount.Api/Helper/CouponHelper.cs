// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Discount.Api.Protos;
using Discount.Application.Dtos;


namespace Discount.Api.Helper
{
    public static class CouponHelper
    {
        public static CouponDTO ToDTO(CouponModel model)
        {
            return new CouponDTO
            {
                Id = Guid.Parse(model.Id),
                CouponName = model.CouponName,
                Description = model.Description,
                Code = model.Code,
                Value = (decimal)model.Value,
                Quantity = model.Quantity,
                StartDate = DateTime.Parse(model.StartDate),
                EndDate = DateTime.Parse(model.EndDate),
                MinimumAmount = (decimal)model.MinimumAmount,
                UsedCount = model.UsedCount
            };
        }

        public static CouponModel ToModel(CouponDTO dto)
        {
            return new CouponModel
            {
                Id = dto.Id.ToString(),
                CouponName = dto.CouponName ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                Code = dto.Code,
                Value = (double)dto.Value,
                Quantity = dto.Quantity,
                StartDate = dto.StartDate.ToString("yyyy-MM-dd"),
                EndDate = dto.EndDate.ToString("yyyy-MM-dd"),
                MinimumAmount = (double?)dto.MinimumAmount ?? 0,
                UsedCount = dto.UsedCount
            };
        }
    }
}
