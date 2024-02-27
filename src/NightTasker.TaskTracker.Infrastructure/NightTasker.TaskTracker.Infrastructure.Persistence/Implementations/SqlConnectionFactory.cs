using System.Data;
using Microsoft.Extensions.Configuration;
using NightTasker.TaskTracker.Infrastructure.Persistence.Contracts;
using Npgsql;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Implementations;

internal sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("Database"));
    }
}