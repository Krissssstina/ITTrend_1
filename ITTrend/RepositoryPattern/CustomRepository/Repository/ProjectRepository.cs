using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace ITTrend.RepositoryPattern.CustomRepository.Repository

{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }
        public Project GetProject(int id)
        {
            return GetEntity(id);
        }
    }
}
