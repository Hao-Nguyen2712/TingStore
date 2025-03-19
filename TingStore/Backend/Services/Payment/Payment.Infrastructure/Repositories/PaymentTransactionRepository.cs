using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payment.Core.Repositories;
using Payment.Infrastructure.Data;

namespace Payment.Infrastructure.Repositories
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentTransactionRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.PaymentTransaction> AddAsync(Core.Entities.PaymentTransaction transaction)
        {
            await _context.AddAsync(transaction);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                throw new Exception("Could not add payment transaction");
            }
            return transaction;
        }
        public async Task<bool> DeletePaymentTransaction(Guid id)
        {
            var trasaction = await _context.PaymentTransactions.FindAsync(id);
            if (trasaction == null)
            {
                return false;
            }
            _context.PaymentTransactions.Remove(trasaction);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }
        public async Task<Core.Entities.PaymentTransaction> GetByIdAsync(Guid id)
        {
            return await _context.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<Core.Entities.PaymentTransaction> GetByOrderIdAsync(Guid orderId)
        {
            return _context.PaymentTransactions.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
        public async Task<List<Core.Entities.PaymentTransaction>> GetPendingTransactionsAsync()
        {
            return await _context.PaymentTransactions.Where(x => x.Status == "Pending").ToListAsync();
        }
        public async Task<bool> UpdateAsync(Core.Entities.PaymentTransaction transaction)
        {
            var transation = await _context.PaymentTransactions.FindAsync(transaction.Id);
            if(transation == null)
            {
                return false;
            }
            _context.Entry(transation).CurrentValues.SetValues(transaction);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
