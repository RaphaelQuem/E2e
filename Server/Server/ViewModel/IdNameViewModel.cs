using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class IdNameViewModel
    {
        public IdNameViewModel(Fornecedor fornecedor)
        {
            this.Id = fornecedor.Id;
            this.Name = fornecedor.Nome;
        }
        public IdNameViewModel(Empresa empresa)
        {
            this.Id = empresa.Id;
            this.Name = empresa.NomeFantasia;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
