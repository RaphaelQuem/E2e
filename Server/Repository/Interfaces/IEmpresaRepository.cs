using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        List<Empresa> GetFornecedorList(int id);
        List<Empresa> GetListNotLinkedFornecedor(int id);
    }
}
