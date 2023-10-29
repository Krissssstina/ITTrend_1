using ITTrend.Models;
using ITTrend.RepositoryPattern.GenericRepository;

namespace ITTrend.RepositoryPattern.CustomRepository.Interfaces
{
    public interface IRoleRepository:IGenericRepository<Role>
    {
        Role GetRole(int id);
    }
}
