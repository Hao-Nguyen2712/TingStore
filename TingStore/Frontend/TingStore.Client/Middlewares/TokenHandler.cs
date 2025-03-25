//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.


//using System.Net.Http.Headers;
//using System.Net;
//using TingStore.Client.Areas.User.Models.Authen;
//using NuGet.Common;

//namespace TingStore.Client.Middlewares
//{
//    public class TokenHandler : DelegatingHandler
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly HttpClient _httpClient;
//        public TokenHandler(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
//        {
//            _httpContextAccessor = httpContextAccessor;
//            _httpClient = httpClientFactory.CreateClient();
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            var bypassUrls = new List<string> { "/User/Auth/Login", "/User/Auth/Register" };

//            // Nếu request là login hoặc register -> không cần token
//            if (bypassUrls.Any(url => request.RequestUri.AbsolutePath.Contains(url, StringComparison.OrdinalIgnoreCase)))
//            {
//                return await base.SendAsync(request, cancellationToken);
//            }

//            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["acessToken"];

//            if (string.IsNullOrEmpty(accessToken))
//            {
//                return RedirectToLogin();
//            }

//            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//            var response = await base.SendAsync(request, cancellationToken);

//            if (response.StatusCode == HttpStatusCode.Unauthorized)
//            {
//                bool hasRetried = _httpContextAccessor.HttpContext?.Items["HasRetried"] as bool? ?? false;
//                if (hasRetried) return RedirectToLogin();

//                _httpContextAccessor.HttpContext.Items["HasRetried"] = true;

//                var newAccessToken = await RefreshTokenAsync();
//                if (!string.IsNullOrEmpty(newAccessToken))
//                {
//                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
//                    response = await base.SendAsync(request, cancellationToken);
//                }
//                else
//                {
//                    return RedirectToLogin();
//                }
//            }

//            return response;
//        }

//        private HttpResponseMessage RedirectToLogin()
//        {
//            var response = new HttpResponseMessage(HttpStatusCode.Redirect);
//            response.Headers.Location = new Uri("/User/Auth/Login", UriKind.Relative);
//            return response;
//        }

//        private async Task<string> RefreshTokenAsync()
//        {
//            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

//            if (string.IsNullOrEmpty(refreshToken))
//            {
//                return string.Empty;
//            }

//            var requestBody = new { RefreshToken = refreshToken };
//            var response = await _httpClient.PostAsJsonAsync("http://localhost:5188/api/auth/refresh-token", requestBody);

//            if (response.IsSuccessStatusCode)
//            {
//                var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

//                if (result != null)
//                {
//                    // Lưu lại token mới
//                    var cookieOptions = new CookieOptions
//                    {
//                        HttpOnly = false,
//                        Secure = true,
//                        Expires = DateTime.UtcNow.AddMinutes(15),
//                        SameSite = SameSiteMode.None
//                    }; var cookieOptions2 = new CookieOptions
//                    {
//                        HttpOnly = false,
//                        Secure = true,
//                        Expires = DateTime.UtcNow.AddDays(1),
//                        SameSite = SameSiteMode.None
//                    };
//                    _httpContextAccessor.HttpContext.Response.Cookies.Append("acessToken", result.Token.AccessToken, cookieOptions);
//                    _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", result.Token.RefreshToken, cookieOptions2);

//                    var emailPrefix = result.Email.Split('@')[0];
//                    _httpContextAccessor.HttpContext.Response.Cookies.Append("email", emailPrefix, cookieOptions);
//                    return result.Token.AccessToken;
//                }
//            }

//            return string.Empty;
//        }
//    }
//}
