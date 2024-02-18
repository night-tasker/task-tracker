using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Commands.CreateOrganization;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Contracts;
using NightTasker.TaskTracker.Core.Application.Features.Organizations.Implementations;
using NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Enums;
using NightTasker.TaskTracker.Core.Domain.Repositories;
using NightTasker.TaskTracker.Infrastructure.Persistence;
using NightTasker.TaskTracker.Infrastructure.Persistence.Repositories;
using NightTasker.TaskTracker.IntegrationTests.Framework;
using Xunit;

namespace NightTasker.TaskTracker.Core.Application.IntegrationTests.Features.Organizations.Commands;

public class CreateOrganizationCommandHandlerTests : ApplicationIntegrationTestsBase
{
    private readonly Faker _faker;

    public CreateOrganizationCommandHandlerTests(TestNpgSql testNpgSql) : base(testNpgSql)
    {
        RegisterService(new ServiceForRegister<IOrganizationsService, OrganizationsService>());
        RegisterService(new ServiceForRegister<IUnitOfWork, UnitOfWork>());
        RegisterService(new ServiceForRegister<CreateOrganizationCommandHandler>());
        BuildServiceProvider();
        PrepareDatabase();
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_CreateOrganization_OrganizationCreated()
    {
        // arrange
        var organizationId = _faker.Random.Guid();
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new CreateOrganizationCommand(organizationId, Array.Empty<CreateOrganizationUserDto>());
            var sut = actScope.ServiceProvider.GetRequiredService<CreateOrganizationCommandHandler>();
            await sut.Handle(command, CancellationToken);
        }
        
        // assert
        await using (var assertScope = CreateAsyncScope())
        {
            var dbContext = assertScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var organization = await dbContext.Set<Organization>()
                .SingleOrDefaultAsync(x => x.Id == organizationId, CancellationToken);
            organization!.Id.Should().Be(organizationId);
        }
    }

    [Fact]
    public async Task Handle_CreateOrganizationWithUsers_UsersAdded()
    {
        // arrange
        var organizationId = _faker.Random.Guid();
        var organizationUsers = new CreateOrganizationUserDto[]
        {
            new(Guid.NewGuid(), Guid.NewGuid(), OrganizationUserRole.Member),
        };
        await using (var arrangeScope = CreateAsyncScope())
        {
            var dbContext = arrangeScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var users = organizationUsers.Select(x => 
                User.CreateInstance(x.UserId, _faker.Random.AlphaNumeric(8)));
            await dbContext.Set<User>().AddRangeAsync(users, CancellationToken);
            await dbContext.SaveChangesAsync(CancellationToken);
        }
        
        // act
        await using (var actScope = CreateAsyncScope())
        {
            var command = new CreateOrganizationCommand(organizationId, organizationUsers);
            var sut = actScope.ServiceProvider.GetRequiredService<CreateOrganizationCommandHandler>();
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
            var organizationUser = organization.OrganizationUsers.First();
            organizationUser.Id.Should().Be(organizationUsers[0].Id);
            organizationUser.UserId.Should().Be(organizationUsers[0].UserId);
            organizationUser.Role.Should().Be(organizationUsers[0].Role);
        }
    }
}