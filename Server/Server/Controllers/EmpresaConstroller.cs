using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Server.ViewModel;
using Service.Interfaces;

namespace Server.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    
    public class EmpresaController : ControllerBase
    {
        IEmpresaService _service;
        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<IdNameViewModel> Get()
        {
            return _service.Get().Select(f => new IdNameViewModel(f)).ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public Empresa Get(int id)
        {
            return _service.GetById(id);
        }

        [Route("ListFornecedor/{id}")]
        [HttpGet]
        public DoubleListViewModel ListFornecedor(int id)
        {
            var notLinked = _service.GetListNotLinkedFornecedor(id).Select(f => new IdNameViewModel(f)).ToList();
            var linked = _service.GetListFornecedor(id).Select(f => new IdNameViewModel(f)).ToList();

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
            _service.Link(fornecedorEmpresa.IdEmpresa, fornecedorEmpresa.IdFornecedor);
            return ListFornecedor(fornecedorEmpresa.IdEmpresa);
        }

        [Route("Unlink")]
        [HttpPost]
        public DoubleListViewModel Unlink(FornecedorEmpresa fornecedorEmpresa)
        {
            _service.Unlink(fornecedorEmpresa.IdEmpresa, fornecedorEmpresa.IdFornecedor);
            return ListFornecedor(fornecedorEmpresa.IdEmpresa);
        }

        [HttpPost]
        public bool Post(Domain.Empresa empresa)
        {
            return _service.Insert(empresa);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

    }
}
