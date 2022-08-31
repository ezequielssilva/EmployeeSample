using System.Data;
using System.Reflection;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeSample.IntegrationTests.Persistence.Data;

public static class SqlFactory
{
    public static (IDbConnection connection, IPersistenceAdapter adapter) Create()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetCallingAssembly())
            .Build();

        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("PersistenceTestsConnectionString"));
        IPersistenceAdapter adapter = new DapperPersistenceAdapter(connection);

        return (connection: connection, adapter: adapter);
    }
}
