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
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        string GET_FORNECEDOR_LIST = " SELECT F.ID, F.NOME FROM FORNECEDOR F INNER JOIN FornecedorEmpresa FE ON F.id = FE.IDFORNECEDOR WHERE FE.IdEmpresa = @ID";
        string GET_UNLINKED_LIST = " SELECT F.ID, F.NOME FROM FORNECEDOR F WHERE F.ID NOT IN(SELECT IDFORNECEDOR FROM FornecedorEmpresa FE WHERE FE.IdEmpresa = @ID)";
        public List<Empresa> GetFornecedorList(int id)
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

        public List<Empresa> GetListNotLinkedFornecedor(int id)
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
