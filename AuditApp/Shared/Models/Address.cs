using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models
{
    [Table("Address")]
    public class Address
    {
        [JsonPropertyName("AddressId")]
        public int AddressId { get; set; }
        [JsonPropertyName("Cep")]
        public string Cep { get; set; } = string.Empty;
        [JsonPropertyName("State")]
        public string State { get; set; } = string.Empty;
        [JsonPropertyName("City")]
        public string City { get; set; } = string.Empty;
        [JsonPropertyName("Neighborhood")]
        public string Neighborhood { get; set; } = string.Empty;
        [JsonPropertyName("Street")]
        public string Street { get; set; } = string.Empty;
        [JsonPropertyName("Number")]
        public int? Number { get; set; }
        [JsonPropertyName("Complement")]
        public string Complement { get; set; } = string.Empty;
    }
}
