using ITTrend.Models;
using ITTrend.RepositoryPattern.GenericRepository;

namespace ITTrend.RepositoryPattern.CustomRepository.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Models.Task>
    {
        Models.Task GetTask(int id);
    }
}
