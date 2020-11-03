

using System;
using System.Data;
using System.Data.SqlClient;

namespace Infra
{
    public static class Factory
    {
        public static IDbConnection GetOpenConnection()
        {
            IDbConnection connection = new SqlConnection(EnvironmentHelper.ConnectionString);
            connection.Open();
            return connection;
        }


    }
}
