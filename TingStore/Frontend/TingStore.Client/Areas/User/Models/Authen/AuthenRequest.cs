// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace TingStore.Client.Areas.User.Models.Authen
{
    public class AuthenRequest
    {
        [Required(ErrorMessage = "Email not null")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Password not null")]
        public String Password { get; set; }
    }
}
