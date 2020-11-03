using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IFornecedorEmpresaRepository : IBaseRepository<FornecedorEmpresa>
    {
        bool Unlink(int IdEmpresa, int IdFornecedor);
    }
}
