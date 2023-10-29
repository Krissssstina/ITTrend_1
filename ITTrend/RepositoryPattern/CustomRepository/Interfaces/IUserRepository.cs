using ITTrend.Models;
using ITTrend.RepositoryPattern.GenericRepository;

namespace ITTrend.RepositoryPattern.CustomRepository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUser(int id);
        User Authenticate(string email, string password);
        bool UserAlreadyInDatabase(string email);
        public bool UserRoleAlreadyInUser(int userId, int roleId);
    }
}
