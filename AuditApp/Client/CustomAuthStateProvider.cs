using AuditApp.Client.Exceptions;
using AuditApp.Shared.Models.Business;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace AuditApp.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _sessionStorageService;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ILocalStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

                if (userSession == null || userSession.ExpirationDate < DateTime.Now)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role),
                    new Claim(ClaimTypes.Name, userSession.Name)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role),
                }));

                userSession.ExpirationDate = DateTime.Now.AddSeconds(userSession.ExpiresInSeconds);

                await _sessionStorageService.SetItemAsync("UserSession", userSession);
            }
            else
            {
                claimsPrincipal = _anonymous;

                await _sessionStorageService.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetLoggedUserEmail()
        {
            try
            {
                var userSession = await _sessionStorageService.GetItemAsync<UserSession>("UserSession");

                if (userSession == null || userSession.ExpirationDate < DateTime.Now)
                {
                    await UpdateAuthenticationState(null);
                    throw new SessionExpiredException("Sessão expirada");
                }

                return userSession.Email;
            }
            catch
            {
                await UpdateAuthenticationState(null);
                throw new SessionExpiredException("Sessão expirada");
            }
        }

    }
}
