using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PNA.Core.Entities;

namespace PNA.AuthService.Infrastructure.Data;


    public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating ( ModelBuilder builder )
        {
            base.OnModelCreating(builder);
        }
    }
    