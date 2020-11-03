using System;

namespace Domain
{
    public class Empresa
    {
        [PK]
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string UF { get; set; }
    }
}
