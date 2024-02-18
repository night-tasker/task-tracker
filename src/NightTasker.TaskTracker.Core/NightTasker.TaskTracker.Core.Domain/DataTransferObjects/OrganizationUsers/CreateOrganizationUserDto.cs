﻿using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Core.Domain.DataTransferObjects.OrganizationUsers;

public sealed record CreateOrganizationUserDto(Guid Id, Guid UserId, OrganizationUserRole Role);