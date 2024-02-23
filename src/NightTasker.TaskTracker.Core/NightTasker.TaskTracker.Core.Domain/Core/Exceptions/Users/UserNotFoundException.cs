using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.TaskTracker.Core.Domain.Core.Exceptions.Users;

public class UserNotFoundException(Guid userId) : NotFoundException(
    $"User with id {userId} not found");