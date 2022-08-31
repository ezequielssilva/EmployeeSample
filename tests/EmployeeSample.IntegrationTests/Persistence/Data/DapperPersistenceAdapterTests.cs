using System.Data;
using Dapper;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Persistence.Queries;
using Xunit.Extensions.Ordering;

namespace EmployeeSample.IntegrationTests.Persistence.Data;

[Order(1)]
public class DapperPersistenceAdapterTests
{
    [Fact, Order(1)]
    public void SuccessWhenConnectionIsOpen()
    {
        (IDbConnection connection, IPersistenceAdapter adapter) = SqlFactory.Create();

        connection.Open();

        Assert.Equal(ConnectionState.Open, connection.State);

        connection.Close();

        Assert.Equal(ConnectionState.Closed, connection.State);
    }

    [Fact, Order(2)]
    public async void SuccessWhenCleanTables()
    {
        (IDbConnection connection, IPersistenceAdapter adapter) = SqlFactory.Create();

        await connection.ExecuteAsync($"DELETE FROM {EmployeeQuery.Table} WHERE [Id] IS NOT NULL");
        int resultEmployee = (await connection.QueryAsync($"SELECT 1 FROM {EmployeeQuery.Table}")).Count();

        Assert.Equal(0, resultEmployee);
    }
}
