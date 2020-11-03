using Domain;
using Infra;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Repository
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
    {
        string GET_FORNECEDOR_LIST = " SELECT E.ID, E.NOMEFANTASIA FROM EMPRESA E INNER JOIN FornecedorEmpresa FE ON E.id = FE.IDEMPRESA WHERE FE.IDFORNECEDOR = @ID";
        string GET_UNLINKED_LIST = " SELECT E.ID, E.NOMEFANTASIA FROM EMPRESA E WHERE E.ID NOT IN(SELECT IDEMPRESA FROM FornecedorEmpresa FE WHERE FE.IDFORNECEDOR = @ID)";
        public List<Empresa> GetEmpresaList(int id)
        {
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = GET_FORNECEDOR_LIST;

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@ID";
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    return cmd.ExecuteReader().ToModel<Empresa>();
                };
            }
        }

        public List<Empresa> GetListNotLinkedEmpresa(int id)
        {
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = GET_UNLINKED_LIST;

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@ID";
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    return cmd.ExecuteReader().ToModel<Empresa>();
                };
            }
        }
    }
}
