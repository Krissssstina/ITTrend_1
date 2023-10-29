using ITTrend.Models;
using ITTrend.RepositoryPattern.GenericRepository;

namespace ITTrend.RepositoryPattern.CustomRepository.Interfaces
{
    public interface IProjectRepository:IGenericRepository<Project>
    {
        Project GetProject(int id);
    }
}
