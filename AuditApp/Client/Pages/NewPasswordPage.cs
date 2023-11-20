using AuditApp.Shared.Models.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace AuditApp.Client.Pages
{
    public partial class NewPasswordPage
    {
        [Parameter]
        public string Email { get; set; } = string.Empty;
        private string _password = string.Empty;
        private string _repeatPassword = string.Empty;

        private async Task CreateNewPassword()
        {
            try
            {
                if (!_password.Equals(_repeatPassword))
                {
                    await JsRuntime.InvokeVoidAsync("alert", "As senhas não coincidem");
                    return;
                }

                var passwordChange = new PasswordChange()
                {
                    Password = _password,
                    Email = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Email))
                };

                var response = await Http.PostAsJsonAsync("password/ChangePassword", passwordChange);

                if (response.IsSuccessStatusCode)
                {
                    await JsRuntime.InvokeVoidAsync("alert", "Senha trocada com sucesso!");

                    NavigationManager.NavigateTo("login");

                    return;
                }
            }
            catch (Exception e)
            {
                await JsRuntime.InvokeVoidAsync("alert", e.Message);
            }
        }
    }
}
