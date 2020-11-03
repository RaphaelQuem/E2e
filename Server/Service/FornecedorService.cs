using Domain;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class FornecedorService : BaseService<Fornecedor>, IFornecedorService
    {
        IFornecedorRepository _repository;
        public FornecedorService(IFornecedorRepository repository): base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Empresa> GetListEmpresa(int id)
        {
            return _repository.GetEmpresaList(id);
        }

        public IEnumerable<Empresa> GetListNotLinkedEmpresa(int id)
        {
            return _repository.GetListNotLinkedEmpresa(id);
        }

        new public bool Insert(Fornecedor model)
        {
            validate(model);
            return base.Insert(model);
        }
        new public bool Update(Fornecedor model)
        {
            validate(model);
            return base.Update(model);
        }
        public void validate(Fornecedor model)
        {
            if (model.PessoaFisica)
            {
                if (string.IsNullOrEmpty(model.RG) || model.Nascimento == null)
                {
                    throw new Exception("Pessoa física necessita de RG e Data de nascimento preenchidos!");
                }
            }
        }
    }
}
