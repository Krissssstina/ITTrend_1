using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace ITTrend.RepositoryPattern.CustomRepository.Repository
{
    public class SprintRepository : GenericRepository<Sprint>, ISprintRepository
    {
        public SprintRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Sprint> GetSprints()
        {
            return GetEntities(q => q.Project);
        }

        public Sprint GetSprint(int id)
        {
            return GetEntity(q => q.Id == id, q => q.Project);
        }
    }
}
