namespace AuditApp.Shared.Models.Repositories
{
    public interface ICompanyRepository
    {
        void Cadastrar(CompanyModel empresa);

        List<CompanyModel> Listar();

        void Atualizar(Guid id, CompanyModel empresa);

        void Deletar(Guid id);

        CompanyModel GetByUserEmail(string email);
    }
}
