using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IFornecedorService : IBaseService<Fornecedor>
    {
        IEnumerable<Empresa> GetListNotLinkedEmpresa(int id);
        IEnumerable<Empresa> GetListEmpresa(int id);
    }
}
