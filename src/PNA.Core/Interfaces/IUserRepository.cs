﻿using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync ( string email );
}