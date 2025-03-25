using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Payment.Application.Commands
{
   public class UpdateTransactionCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }

        public UpdateTransactionCommand() { }
        public UpdateTransactionCommand(Guid id, string transactionId, string status)
        {
            Id = id;
            TransactionId = transactionId;
            Status = status;
        }
    }
}
