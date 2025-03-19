using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payment.Core.Entities;

namespace Payment.Core.Repositories
{
   public interface IPaymentTransactionRepository
    {
        Task<PaymentTransaction> GetByIdAsync(Guid id);
        Task<PaymentTransaction> GetByOrderIdAsync(Guid orderId);
        Task<PaymentTransaction> AddAsync(PaymentTransaction transaction);
        Task<bool> UpdateAsync(PaymentTransaction transaction);
        Task<List<PaymentTransaction>> GetPendingTransactionsAsync();
        Task<bool> DeletePaymentTransaction(Guid id);
    }
}
