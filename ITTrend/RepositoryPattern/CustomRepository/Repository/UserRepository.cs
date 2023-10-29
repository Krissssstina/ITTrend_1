using ITTrend.Context;
using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ITTrend.RepositoryPattern.CustomRepository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
        public User GetUser(int id)
        {
            return GetEntity(q => q.Id==id, q => q.UserRoles, q => q.Tasks);

        }

        public User Authenticate(string email, string password)
        {
            return GetEntity(q => q.Email == email && q.Password == password, q => q.UserRoles);

        }
        public bool UserAlreadyInDatabase(string email)
        {
            return GetEntities().Any(q => q.Email == email);

        }

        public bool UserRoleAlreadyInUser(int userId, int roleId)
        {
            IEnumerable<User> list =GetEntities(q => q.UserRoles.Find(c => c.UserId == userId && c.RoleId == roleId));
            if (list == null) return false;
            return true;

        }
    }
}
