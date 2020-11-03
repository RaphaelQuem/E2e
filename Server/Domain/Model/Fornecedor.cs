using System;

namespace Domain
{
    public class Fornecedor
    {
        [PK]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public bool PessoaFisica { get; set; }
        public string RG { get; set; }
        public DateTime? Nascimento { get; set; }
    }
}
