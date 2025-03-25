// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TingStore.Client.Areas.Admin.Models.Order;

namespace TingStore.Client.Areas.Admin.Models.Dashboard
{
    public class DashboardVM
    {
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
