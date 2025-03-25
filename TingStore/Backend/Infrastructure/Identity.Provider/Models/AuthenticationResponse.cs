// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Identity.Provider.Models
{
    public class AuthenticationResponse
    {
        public String Email { get; set; }
        public TokenModel Token { get; set; }
    }
}
