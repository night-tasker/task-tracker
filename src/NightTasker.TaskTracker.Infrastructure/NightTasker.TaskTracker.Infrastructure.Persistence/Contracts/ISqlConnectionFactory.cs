using System.Data;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Contracts;

public interface ISqlConnectionFactory
{
    IDbConnection GetConnection();
}