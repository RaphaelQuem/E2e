using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FornecedorEmpresa
    {
        public FornecedorEmpresa()
        {

        }
        public FornecedorEmpresa(int idEmpresa, int idFornecedor)
        {
            this.IdEmpresa = idEmpresa;
            this.IdFornecedor = idFornecedor;
        }
        [PK]
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdFornecedor { get; set; }
    }
}
