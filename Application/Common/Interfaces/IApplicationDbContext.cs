using Application.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        DbSet<UserRole> UserRoles { get; set; }

        Task<int> SaveChangesAsync();
    }
}
