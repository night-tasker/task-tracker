using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Core.Application.Exceptions.NotFound;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Commands.AddOrganizationUser;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.OrganizationUsers.Implementations;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Enums;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence;
using NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;
using NightTasker.TaskTracker.IntegrationTests.Framework;
using Xunit;

namespace NightTasker.TaskTracker.Core.Application.IntegrationTests.Features.OrganizationUsers.Commands;

public class AddOrganizationUserCommandHandlerTests : ApplicationIntegrationTestsBase
{
    private readonly Faker _faker;

    public AddOrganizationUserCommandHandlerTests(TestNpgSql testNpgSql) : base(testNpgSql)
    {
        RegisterService(new ServiceForRegister<IOrganizationUsersService, OrganizationUsersService>());
        RegisterService(new ServiceForRegister<IUnitOfWork, UnitOfWork>());
        RegisterService(new ServiceForRegister<AddOrganizationUserCommandHandler>());
        BuildServiceProvider();
        PrepareDatabase();
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_OrganizationDoesNotExist_OrganizationNotFoundException()
    {
        // arrange
        var organizationId = _faker.Random.Guid();
        var userId = _faker.Random.Guid();
        var expectedException = new OrganizationNotFoundException(organizationId);
        
        await using (var arrangeScope = CreateAsyncScope())
        {
            var dbContext = arrangeScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = User.CreateInstance(userId, _faker.Name.FirstName());
            await dbContext.Set<User>().AddAsync(user, CancellationToken);
            await dbContext.SaveChangesAsync(CancellationToken);
        }
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new AddOrganizationUserCommand(
                Guid.NewGuid(), organizationId, userId, OrganizationUserRole.Member);
            var sut = actScope.ServiceProvider.GetRequiredService<AddOrganizationUserCommandHandler>();
            
            var act = async () => await sut.Handle(command, CancellationToken);
            
            // assert
            var actualException = await act.Should().ThrowAsync<OrganizationNotFoundException>();
            actualException.WithMessage(expectedException.Message);
        }
    }
    
    [Fact]
    public async Task Handle_UserDoesNotExist_UserNotFoundException()
    {
        // arrange
        var organizationId = _faker.Random.Guid();
        var userId = _faker.Random.Guid();
        var expectedException = new UserNotFoundException(userId);
        
        await using (var arrangeScope = CreateAsyncScope())
        {
            var dbContext = arrangeScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var organization = Organization.CreateInstance(organizationId);
            await dbContext.Set<Organization>().AddAsync(organization, CancellationToken);
            await dbContext.SaveChangesAsync(CancellationToken);
        }
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new AddOrganizationUserCommand(
                Guid.NewGuid(), organizationId, userId, OrganizationUserRole.Member);
            var sut = actScope.ServiceProvider.GetRequiredService<AddOrganizationUserCommandHandler>();
            
            var act = async () => await sut.Handle(command, CancellationToken);
            
            // assert
            var actualException = await act.Should().ThrowAsync<UserNotFoundException>();
            actualException.WithMessage(expectedException.Message);
        }
    }

    [Theory]
    [InlineData(OrganizationUserRole.Member)]
    [InlineData(OrganizationUserRole.Admin)]
    public async Task Handle_OrganizationAndUserExist_OrganizationUserCreated(OrganizationUserRole role)
    {
        // arrange
        var organizationId = _faker.Random.Guid();
        var userId = _faker.Random.Guid();
        var organizationUserId = _faker.Random.Guid();
        await using (var arrangeScope = CreateAsyncScope())
        {
            var dbContext = arrangeScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = User.CreateInstance(userId, _faker.Name.FirstName());
            await dbContext.Set<User>().AddAsync(user, CancellationToken);
            var organization = Organization.CreateInstance(organizationId);
            await dbContext.Set<Organization>().AddAsync(organization, CancellationToken);
            await dbContext.SaveChangesAsync(CancellationToken);
        }
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new AddOrganizationUserCommand(
                organizationUserId, organizationId, userId, role);
            var sut = actScope.ServiceProvider.GetRequiredService<AddOrganizationUserCommandHandler>();
            await sut.Handle(command, CancellationToken);
        }
        
        // assert
        await using (var assertScope = CreateAsyncScope())
        {
            var dbContext = assertScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var organization = await dbContext.Set<Organization>()
                .Include(x => x.OrganizationUsers)
                .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == organizationId, CancellationToken);
            organization!.Id.Should().Be(organizationId);

            organization.OrganizationUsers.Should().HaveCount(1);
            var createdOrganizationUser = organization.OrganizationUsers.First();
            createdOrganizationUser.Id.Should().Be(organizationUserId);
            createdOrganizationUser.Role.Should().Be(role);
            createdOrganizationUser.User.Id.Should().Be(userId);
        }
    }
}