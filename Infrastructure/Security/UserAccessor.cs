﻿using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext) : IUserAccesser
    {
        public async Task<User> GetUserAsync()
        {
            return await dbContext.Users.FindAsync(GetUserId())
                ?? throw new UnauthorizedAccessException("No user is Logged in");
        }

        public string GetUserId()
        {
            return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("No user found");
        }
    }
}
