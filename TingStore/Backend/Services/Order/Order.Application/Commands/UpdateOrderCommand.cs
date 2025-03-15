using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Commands
{
    public class UpdateOrderCommand : IRequest<OrderDTO>
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public decimal TotalAmount { get; set; }

        public List<ItemUpdate>? Item { get; set; }


        public UpdateOrderCommand(Guid id, string code, decimal toTal, List<ItemUpdate> itemUpdates)
        {
            Id = id;
            Code = code;
            TotalAmount = toTal;
            Item = itemUpdates;
        }

    }

    public class ItemUpdate
    {
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public bool IsDelete { get; set; }
    }
}
