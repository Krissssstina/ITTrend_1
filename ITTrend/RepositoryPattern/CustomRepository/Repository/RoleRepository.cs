using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ITTrend.RepositoryPattern.CustomRepository.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public Role GetRole(int id)
        {
           return GetEntity(id);
        }
    }
}
