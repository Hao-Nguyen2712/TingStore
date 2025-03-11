using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Core.Entities;

namespace Discount.Core.Repositories
{
    public interface ICouponRepository
    {
        public Task<Coupon> AddCoupon(Coupon coupon);
        public Task<Coupon> GetCouponByName(string coupoName);
        public Task<bool> UpdateCoupon(Coupon coupon);
        public Task DeleteCoupon(string couponName);
        public Task<Coupon> GetCouponByCode(string code);
        public Task<List<Coupon>> GetCoupons();
    }
}
