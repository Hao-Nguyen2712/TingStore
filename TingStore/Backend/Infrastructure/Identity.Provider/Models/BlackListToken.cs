// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace Identity.Provider.Models
{
    public class BlackListToken
    {
        [Key]
        
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime BlacklistedAt { get; set; }
        public DateTime? ExpiryTime { get; set; }
    }
}
