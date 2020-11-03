using Domain;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class EmpresaService : BaseService<Empresa>, IEmpresaService
    {
        IEmpresaRepository _repository;
        IFornecedorRepository _fornecedorRepository;
        IFornecedorEmpresaRepository _fornecedorEmpresaRepository;
        public EmpresaService(IEmpresaRepository repository, 
            IFornecedorEmpresaRepository feRepository,
            IFornecedorRepository fornecedorRepository
            ) : base(repository)
        {
            _repository = repository;
            _fornecedorEmpresaRepository = feRepository;
            _fornecedorRepository = fornecedorRepository;
        }

        public List<Empresa> GetListFornecedor(int id)
        {
            return _repository.GetFornecedorList(id);
        }

        public List<Empresa> GetListNotLinkedFornecedor(int id)
        {
            return _repository.GetListNotLinkedFornecedor(id);
        }

        public bool Link(int idEmpresa, int idFornecedor)
        {
            var empresa = _repository.Select(idEmpresa);
            var fornecedor = _fornecedorRepository.Select(idFornecedor);
                
            if(empresa.UF.Equals("PR") && fornecedor.PessoaFisica)
            {
                
                int age = DateTime.Now.Year - ((DateTime)fornecedor.Nascimento).Year;
                if (DateTime.Now.DayOfYear < ((DateTime)fornecedor.Nascimento).DayOfYear)
                    age -= 1;

                if(age < 18)
                {
                    throw new Exception("Não é possível cadastrar um menor de idade como fornecedor para o Paraná");
                }
            }

            return _fornecedorEmpresaRepository.Insert(new FornecedorEmpresa(idEmpresa, idFornecedor));
        }

        public bool Unlink(int idEmpresa, int idFornecedor)
        {
            return _fornecedorEmpresaRepository.Unlink(idEmpresa, idFornecedor);
        }
    }
}
