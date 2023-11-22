using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models
{
    [Table("Companies")]
    public class CompanyModel
    {
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        [JsonPropertyName("NomeFantasia")]
        public string? NomeFantasia { get; set; }
        [JsonPropertyName("Cnpj")]
        public string? Cnpj { get; set; }
        [JsonPropertyName("RazaoSocial")]
        public string? RazaoSocial { get; set; }
        [JsonPropertyName("Endereco")]
        public string? Endereco { get; set; }
        [JsonPropertyName("Address")]
        public Address Address { get; set; } = new Address();
        [JsonPropertyName("AssociatedEmail")]
        public string AssociatedEmail { get; set; } = string.Empty;

        [JsonPropertyName("AddressId")]
        public int AddressId { get; set; }
    }
}
