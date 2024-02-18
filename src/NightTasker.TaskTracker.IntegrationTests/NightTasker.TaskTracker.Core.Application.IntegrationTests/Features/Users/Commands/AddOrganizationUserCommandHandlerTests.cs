using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Core.Application.Features.Users.Commands.CreateUser;
using NightTasker.TaskTracker.Core.Application.Features.Users.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Users.Implementations;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence;
using NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;
using NightTasker.TaskTracker.IntegrationTests.Framework;
using Xunit;

namespace NightTasker.TaskTracker.Core.Application.IntegrationTests.Features.Users.Commands;

public class AddOrganizationUserCommandHandlerTests : ApplicationIntegrationTestsBase
{
    private readonly Faker _faker;

    public AddOrganizationUserCommandHandlerTests(TestNpgSql testNpgSql) : base(testNpgSql)
    {
        RegisterService(new ServiceForRegister<IUsersService, UsersService>());
        RegisterService(new ServiceForRegister<IUnitOfWork, UnitOfWork>());
        RegisterService(new ServiceForRegister<AddOrganizationUserCommandHandler>());
        BuildServiceProvider();
        PrepareDatabase();
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_CreateUser_UserCreated()
    {
        // arrange
        var userId = _faker.Random.Guid();
        var userName = _faker.Random.AlphaNumeric(8);
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new CreateUserCommand(userId, userName);
            var sut = actScope.ServiceProvider.GetRequiredService<AddOrganizationUserCommandHandler>();
            await sut.Handle(command, CancellationToken);
        }
        
        // assert
        await using (var assertScope = CreateAsyncScope())
        {
            var dbContext = assertScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = await dbContext.Set<User>()
                .SingleOrDefaultAsync(x => x.Id == userId, CancellationToken);
            user!.Id.Should().Be(userId);
            user.UserName.Should().Be(userName);
        }
    }
}