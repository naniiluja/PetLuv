using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace PetLuv.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleConnection")
                ?? throw new InvalidOperationException("Connection string 'OracleConnection' not found.");
        }

        public IDbConnection CreateConnection()
            => new OracleConnection(_connectionString);
    }
}