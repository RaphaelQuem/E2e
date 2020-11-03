using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.ViewModel;
using Service.Interfaces;

namespace Server.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    
    public class FornecedorController : ControllerBase
    {
        IFornecedorService _fornecedorService;
        IEmpresaService _empresaService;
        public FornecedorController(IFornecedorService fornecedorService, IEmpresaService empresaService)
        {
            _fornecedorService = fornecedorService;
            _empresaService = empresaService;
        }

        [Route("filter/{filter}")]
        [HttpGet]
        public List<Fornecedor> Filter(string filter)
        {
            return _fornecedorService.Get().Where(f =>
                f.Nome.Contains(filter) ||
                f.Documento.Replace(".","").Replace("-","").Replace("/","").Contains(filter)
            ).ToList();
        }


        [HttpGet]
        public List<IdNameViewModel> Get()
        {
            return _fornecedorService.Get().Select(f => new IdNameViewModel(f)).ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public Fornecedor Get(int id)
        {
            return _fornecedorService.GetById(id);
        }

        [HttpPost]
        public bool Post(Fornecedor fornecedor)
        {
            return _fornecedorService.Insert(fornecedor);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _fornecedorService.Delete(id);
        }

        [Route("ListEmpresa/{id}")]
        [HttpGet]
        public DoubleListViewModel ListEmpresa(int id)
        {
            var notLinked = _fornecedorService.GetListNotLinkedEmpresa(id).Select(f => new IdNameViewModel(f)).ToList();
            var linked = _fornecedorService.GetListEmpresa(id).Select(f => new IdNameViewModel(f)).ToList();
            return new DoubleListViewModel
            {
                Linked = linked,
                NotLinked = notLinked
            };


        }

        [Route("Link")]
        [HttpPost]
        public DoubleListViewModel Link(FornecedorEmpresa fornecedorEmpresa)
        {
            _empresaService.Link(fornecedorEmpresa.IdEmpresa, fornecedorEmpresa.IdFornecedor);
            return ListEmpresa(fornecedorEmpresa.IdFornecedor);
        }

        [Route("Unlink")]
        [HttpPost]
        public DoubleListViewModel Unlink(FornecedorEmpresa fornecedorEmpresa)
        {
            _empresaService.Unlink(fornecedorEmpresa.IdEmpresa, fornecedorEmpresa.IdFornecedor);
            return ListEmpresa(fornecedorEmpresa.IdFornecedor);
        }

    }
}
