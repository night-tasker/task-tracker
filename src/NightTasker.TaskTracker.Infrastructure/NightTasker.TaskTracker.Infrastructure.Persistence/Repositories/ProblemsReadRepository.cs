using Dapper;
using NightTasker.TaskTracker.Core.Application.Features.Problems.Models;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence.Contracts;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;

internal sealed class ProblemsReadRepository(ISqlConnectionFactory sqlConnectionFactory) : IProblemsReadRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = 
        sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));

    public async Task<IReadOnlyCollection<Problem>> GetProblemsAssignedOnUser(
        Guid userId, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetConnection();
        var sql = $"SELECT * FROM problems WHERE assignee_id = @UserId";
        var problems = await connection.QueryAsync<Problem>(sql, new { UserId = userId });
        return problems.ToArray();
    }
}