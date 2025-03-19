// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TingStore.Client.Areas.User.Models.Reviews
{
    public class ReviewResponse
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserFullName { get; set; } 
        public string UserAvatar { get; set; }
    }
}
