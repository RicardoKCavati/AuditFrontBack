using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Company : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public Company(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterCompany(CompanyModel empresa)
        {
            try
            {
                _companyRepository.Cadastrar(empresa);

                return StatusCode(201);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Administrador")]
        public IActionResult Get()
        {
            try
            {

                List<CompanyModel> ListarEmpresas = _companyRepository.Listar();
                return StatusCode(200, ListarEmpresas);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("GetCompanyIdByEmail")]
        public ActionResult<string> GetCompanyIdByEmail([FromBody] string email)
        {
            try
            {
                var company = _companyRepository.GetByUserEmail(email);

                if (company == null)
                {
                    return StatusCode(500, "Não foi cadastrada nenhuma organização associada ao seu e-mail, as perguntas não serão carregadas.");
                }

                return company.CompanyId.ToString();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("ObtainCompanyByEmail")]
        public ActionResult<CompanyModel> ObtainCompanyByEmail([FromBody] string email)
        {
            try
            {
                var company = _companyRepository.GetByUserEmail(email);

                if (company == null)
                {
                    return NotFound();
                }

                return company;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(CompanyModel empresa)
        {
            try
            {
                _companyRepository.Atualizar(empresa);

                return StatusCode(200);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _companyRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
