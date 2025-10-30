using Microsoft.AspNetCore.Identity;

namespace Ergo.Fit.Models
{
    public class ApplicationUser : IdentityUser
    {
        //  Propriedades adicionais customizadas
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        //  Relacionamentos (se precisar)
        // public int? EmpresaId { get; set; }
        // public EmpresaModel? Empresa { get; set; }
    }
}
