using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cep : ControllerBase
    {
        [HttpPost("GetCepInfo")]
        public async ValueTask<ActionResult<Address>> PostTodoItem([FromBody] string cep)
        {
            var errorMessage = string.Empty;
            AddressResponse? addressResponse = null;
            try
            {
                var htppClient = new HttpClient();

                var uri = $"https://viacep.com.br/ws/{cep.Replace("-", string.Empty)}/json";

                var response = await htppClient.GetAsync(uri);


                if (response.IsSuccessStatusCode)
                {
                    addressResponse = JsonSerializer.Deserialize<AddressResponse>(await response.Content.ReadAsStreamAsync());
                }
                else
                {
                    return StatusCode(404, "Cep inválido");
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            if (addressResponse is not null)
            {
                return StatusCode(200, new Address()
                {
                    Cep = addressResponse.Cep,
                    City = addressResponse.Localidade,
                    State = addressResponse.Uf,
                    Neighborhood = addressResponse.Bairro,
                    Street = addressResponse.Logradouro
                });
            }
            else
            {
                return StatusCode(500, errorMessage);
            }
        }
    }
}
