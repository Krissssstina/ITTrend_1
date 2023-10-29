using ITTrend.RepositoryPattern.CustomRepository.Interfaces;

namespace ITTrend.RepositoryPattern.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; set; }
        IProjectRepository ProjectRepository { get; set; }
        ISprintRepository SprintRepository { get; set; }
        ITaskRepository TaskRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        int Save();

    }
}
