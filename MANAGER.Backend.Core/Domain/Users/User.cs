﻿using MANAGER.Backend.Core.Domain.Base;

namespace MANAGER.Backend.Core.Domain.Users;

public class User : EntityBase
{
    public required string Name { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public int Age { get; set; }
}