using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.DTOs;

namespace Order.Application.Services
{
    public interface IDiscountClientService
    {
        public Task<ValueCouponDTO> GetValue(string code , decimal total);
    }
}
