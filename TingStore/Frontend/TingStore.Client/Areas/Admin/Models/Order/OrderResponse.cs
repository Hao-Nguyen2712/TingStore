// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TingStore.Client.Areas.Admin.Models.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string? DiscountId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string? Status { get; set; }
    }
}
