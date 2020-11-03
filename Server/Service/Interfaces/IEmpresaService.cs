using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IEmpresaService : IBaseService<Empresa>
    {
        List<Empresa> GetListFornecedor(int id);
        List<Empresa> GetListNotLinkedFornecedor(int id);
        bool Link(int idEmpresa, int idFornecedor);
        bool Unlink(int idEmpresa, int idFornecedor);
    }
}
