using Domain;
using Infra;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Repository
{
    public class FornecedorEmpresaRepository : BaseRepository<FornecedorEmpresa>, IFornecedorEmpresaRepository
    {
        string UNLINK = "DELETE FROM FORNECEDOREMPRESA WHERE IDEMPRESA = @IDEMPRESA AND IDFORNECEDOR = @IDFORNECEDOR";
        public bool Unlink(int IdEmpresa, int IdFornecedor)
        {

            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = UNLINK;

                    var pEmpresaId = cmd.CreateParameter();
                    pEmpresaId.ParameterName = "@IDEMPRESA";
                    pEmpresaId.Value = IdEmpresa;
                    cmd.Parameters.Add(pEmpresaId);

                    var pFornecedorId = cmd.CreateParameter();
                    pFornecedorId.ParameterName = "@IDFORNECEDOR";
                    pFornecedorId.Value = IdFornecedor;
                    cmd.Parameters.Add(pFornecedorId);

                    return cmd.ExecuteNonQuery() > 0;
                };
            }

        }
    }
}
