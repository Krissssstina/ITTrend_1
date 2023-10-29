using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace ITTrend.RepositoryPattern.CustomRepository.Repository
{
    public class TaskRepository : GenericRepository<Models.Task>, ITaskRepository
    {
        public TaskRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Models.Task> GetTasks()
        {
            return GetEntities(q => q.Sprint);
        }

        public Models.Task GetTask(int id)
        {
            return GetEntity(q => q.Id == id, q => q.Sprint, q => q.User, q => q.Files);
        }
    }
}
