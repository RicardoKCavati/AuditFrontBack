using System.ComponentModel.DataAnnotations.Schema;

namespace AuditApp.Shared.Models
{
    [Table("Company")]
    public class CompanyModel
    {
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
        public string? Endereco { get; set; }
        public Address Address { get; set; } = new Address();
        public string AssociatedEmail { get; set; } = string.Empty;
    }
}
