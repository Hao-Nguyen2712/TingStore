using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Core.Entities;

namespace Discount.Infrastructure.Db.DataSeeding
{
    public static class CouponSeeding
    {
        public static void Seed(DiscountDbContext context)
        {
            if (!context.Coupons.Any())
            {
                var coupons = new List<Coupon>
            {
                new Coupon
                {
                    Id = Guid.NewGuid(),
                    CouponName = "Giảm 10% cho đơn hàng đầu tiên",
                    Code = "NEW10",
                    Value = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    Description = "Áp dụng cho khách hàng mới",
                    Quantity = 100,
                    UsedCount = 0,
                    MinimumAmount = 100
                },
                new Coupon
                {
                    Id = Guid.NewGuid(),
                    CouponName = "Giảm 50K cho đơn từ 500K",
                    Code = "SALE50",
                    Value = 50,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(2),
                    Description = "Giảm ngay 50K khi đơn hàng từ 500K",
                    Quantity = 200,
                    UsedCount = 0,
                    MinimumAmount = 500
                }
            };

                context.Coupons.AddRange(coupons);
                context.SaveChanges();
            }
        }
    }
}
