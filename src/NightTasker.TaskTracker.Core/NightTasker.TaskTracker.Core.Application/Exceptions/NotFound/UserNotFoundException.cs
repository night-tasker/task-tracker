using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.TaskTracker.Core.Application.Exceptions.NotFound;

public class UserNotFoundException(Guid userId) : NotFoundException(
    $"User with id {userId} not found");