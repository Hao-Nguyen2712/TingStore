using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DiscountDbContext _context;

        public CouponRepository(DiscountDbContext context)
        {
            _context = context;
        }

        public async Task<Coupon> AddCoupon(Coupon coupon)
        {
            if (coupon != null)
            {
                await _context.Coupons.AddAsync(coupon);
                await _context.SaveChangesAsync();
                return coupon;
            }
            throw new Exception("Coupon is null");
        }
        public async Task DeleteCoupon(string couponName)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName);
            if (coupon == null)
            {
                throw new Exception("Coupon null");
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }
        public async Task<Coupon> GetCouponByName(string couponName)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponName == couponName)
                ?? throw new Exception("Coupon null");
        }
        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var couponExist = await _context.Coupons.FindAsync(coupon.Id);
            if (couponExist == null)
            {
                return false;
            }
           _context.Entry(couponExist).CurrentValues.SetValues(coupon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
