using ITTrend.Context;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.CustomRepository.Repository;
using Microsoft.EntityFrameworkCore;

namespace ITTrend.RepositoryPattern.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        public IRoleRepository RoleRepository { get; set; }
        public IProjectRepository ProjectRepository { get; set; }
        public ISprintRepository SprintRepository { get; set; }
        public ITaskRepository TaskRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public int Save() => _dbContext.SaveChanges();
        public void Dispose() => _dbContext.Dispose();
        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(null, nameof(dbContext));

            RoleRepository = new RoleRepository(_dbContext);
            ProjectRepository = new ProjectRepository(_dbContext);
            SprintRepository = new SprintRepository(_dbContext);
            TaskRepository = new TaskRepository(_dbContext);
            UserRepository = new UserRepository(_dbContext);
        }
    }
}
