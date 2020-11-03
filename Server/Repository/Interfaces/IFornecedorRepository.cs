using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IFornecedorRepository : IBaseRepository<Fornecedor>
    {
        List<Empresa> GetEmpresaList(int id);
        List<Empresa> GetListNotLinkedEmpresa(int id);
    }
}
