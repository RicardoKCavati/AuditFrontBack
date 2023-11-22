using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Database.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AuditContext _auditContext;

        public CompanyRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public void Atualizar(CompanyModel empresa)
        {
            var empresaBuscada = _auditContext.Companies.FirstOrDefault(x => x.CompanyId == empresa.CompanyId)!;

            if (empresaBuscada != null)
            {
                empresaBuscada.NomeFantasia = empresa.NomeFantasia;
                empresaBuscada.Cnpj = empresa.Cnpj;
                empresaBuscada.RazaoSocial = empresa.RazaoSocial;
                empresaBuscada.Endereco = empresa.Endereco;
                empresaBuscada.Address = empresa.Address;

            }

            _auditContext.Companies.Update(empresaBuscada!);

            _auditContext.SaveChanges();
        }

        public void Cadastrar(CompanyModel companyModel)
        {
            _auditContext.Companies.Add(companyModel);
            _auditContext.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var empresa = _auditContext.Companies.FirstOrDefault(x => x.CompanyId == id)!;

            _auditContext.Companies.Remove(empresa);

            _auditContext.SaveChanges();
        }

        public CompanyModel GetByUserEmail(string email)
        {
            var company = _auditContext.Companies.FirstOrDefault(c => c.AssociatedEmail.Equals(email));

            if (company == null)
            {
                return null;
            }

            company.Address = _auditContext.Addresses.FirstOrDefault(a => a.AddressId == company.AddressId);

            return company;
        }

        public List<CompanyModel> Listar()
        {
            return _auditContext.Companies.ToList();
        }
    }
}
