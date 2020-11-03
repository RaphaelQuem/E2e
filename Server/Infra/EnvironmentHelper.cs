using Microsoft.Extensions.Configuration;
using System;
using System.Security.Policy;

namespace Infra
{
    public static class EnvironmentHelper
    {
        private static string _connectionString;
        public static void SetConfiguration(IConfiguration  config)
        {
            _connectionString = config["ConnectionString"];
        }
        public static string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }
    }
}
