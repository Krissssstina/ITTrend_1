using ITTrend.Models;
using ITTrend.RepositoryPattern.GenericRepository;

namespace ITTrend.RepositoryPattern.CustomRepository.Interfaces
{
    public interface ISprintRepository : IGenericRepository<Sprint>
    {
        Sprint GetSprint(int id);
    }
}
