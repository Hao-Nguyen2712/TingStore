using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payment.Core.Entities;

namespace Payment.Infrastructure.Data.Seeding
{
    public static class PaymentSeed
    {
        public static void Seed(PaymentDbContext context)
        {
            // Kiểm tra nếu bảng đã có dữ liệu, không seed lại
            if (context.PaymentTransactions.Any())
            {
                return;
            }

            var transactions = new List<PaymentTransaction>
            {
                new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    OrderId = Guid.NewGuid(),
                    Amount = 100000m, // 100,000 VNĐ
                    PaymentMethod = "VNPay",
                    TransactionId = "VNPAY123456",
                    Status = "Success",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = DateTime.UtcNow
                },
                new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    OrderId = Guid.NewGuid(),
                    Amount = 50000m, // 50,000 VNĐ
                    PaymentMethod = "VNPay",
                    TransactionId = null,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                },
                new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    OrderId = Guid.NewGuid(),
                    Amount = 200000m, // 200,000 VNĐ
                    PaymentMethod = "VNPay",
                    TransactionId = "VNPAY789101",
                    Status = "Failed",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            context.PaymentTransactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}

