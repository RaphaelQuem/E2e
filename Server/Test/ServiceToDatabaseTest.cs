using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Service.Interfaces;
using Service;
using Repository;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ServiceToDatabaseTest
    {
        IFornecedorService _service;
        public ServiceToDatabaseTest()
        {
            _service = new FornecedorService(new FornecedorRepository());
        }
        [TestMethod]
        public void ShouldTestFornecedorBaseCrud()
        {

            #region Nonexistent Select
            var model = _service.GetById(-1);
            Assert.IsNull(model); 
            #endregion

            #region Insert
            Fornecedor fornecedor = new Fornecedor();

            fornecedor.Nome = "Teste";
            fornecedor.Documento = "000.000.001-91";
            fornecedor.Nascimento = DateTime.Now;
            fornecedor.PessoaFisica = true;
            fornecedor.RG = "88.888.888-4";
            fornecedor.Email = "Fornecedor@fornecimento.com";

            var insertResult = _service.Insert(fornecedor);

            Assert.IsTrue(insertResult);

            #endregion

            #region SelectAll
            var fornecedorList = _service.Get();

            Assert.IsNotNull(fornecedorList);
            Assert.IsTrue(fornecedorList.Count > 0);
            #endregion

            #region Select
            fornecedor = _service.GetById(fornecedorList.First().Id);

            Assert.IsNotNull(fornecedor);
            #endregion

            #region Update
            fornecedor.Nome = "Updated";
            fornecedor.PessoaFisica = false;

            _service.Update(fornecedor);

            var dbItem = _service.GetById(fornecedor.Id);

            Assert.AreEqual(dbItem.Nome, fornecedor.Nome);
            Assert.IsFalse(dbItem.PessoaFisica);
            Assert.AreEqual(dbItem.Id, fornecedor.Id);
            #endregion

            #region Delete
            fornecedor = fornecedorList.First();
            var deleteResult = _service.Delete(fornecedor.Id);

            Assert.IsTrue(deleteResult);
            #endregion

        }


    }
}
