// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TingStore.Client.Areas.User.Models.Authen
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public TokenModel Token { get; set; }
    }

    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
