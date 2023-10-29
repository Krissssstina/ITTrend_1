using ITTrend.Dto;
using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.CustomRepository.Repository;
using ITTrend.RepositoryPattern.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace ITTrend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IUserRepository UserRepository => _unitOfWork.UserRepository;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IEnumerable<UserDto> Get()
        {
            return UserRepository.GetEntities().Adapt<IEnumerable<UserDto>>();
        }

        [HttpGet]
        [Route("get-user")]
        [Authorize(Roles = "Admin,Manager")]
        public UserDto GetUser(int id)
        {
            return UserRepository.GetUser(id).Adapt<UserDto>();
        }

        [HttpGet]
        [Route("get-userRoles")]
        [Authorize(Roles = "Admin,Manager")]
        public List<UserRole> GetUserRoles(int id)
        {
            var user = UserRepository.GetUser(id);
            if (user == null) return null;
            return user.UserRoles;
        }
       

        [HttpPost]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Post(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName

            };
            UserRepository.Insert(user);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public void Put(UserDto userDto)
        {
            var user = userDto.Id == 0 ? new User() : UserRepository.GetUser(userDto.Id);
            if (user == null) return;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;

            UserRepository.InsertOrUpdate(user);
            _unitOfWork.Save();
        }
        [HttpPut]
        [Route("add-role")]
        [Authorize(Roles = "Admin")]
        public void PutRole(UserRoleDto dto)
        {
            var user = UserRepository.GetUser(dto.UserId);
            if (user == null) return;
            var role = new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            };
            List<UserRole> list = user.UserRoles;
            if (list == null) list = new List<UserRole>();
            if (UserRepository.UserRoleAlreadyInUser(dto.UserId, dto.RoleId) == false) list.Add(role);

            UserRepository.InsertOrUpdate(user);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Route("delete-role")]
        [Authorize(Roles = "Admin")]
        public void DeleteRole(UserRoleDto dto)
        {
            var user = UserRepository.GetUser(dto.UserId);
            if (user == null) return;
            var role = user.UserRoles.Find(q => q.Id == dto.Id);
            user.UserRoles.Remove(role);

            UserRepository.InsertOrUpdate(user);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Route("add-task")]
        [Authorize(Roles = "Admin,Manager")]
        public void PutTask(int idTask, int idUser)
        {
            var user = UserRepository.GetUser(idUser);
            if (user == null) return;
            var task = _unitOfWork.TaskRepository.GetTask(idTask);
            List<Models.Task> tasks = user.Tasks;
            if (tasks == null) tasks = new List<Models.Task>();
            tasks.Add(task);

            UserRepository.InsertOrUpdate(user);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Route("delete-task")]
        [Authorize(Roles = "Admin,Manager")]
        public void DeleteTask(int idTask, int idUser)
        {
            var user = UserRepository.GetUser(idUser);
            if (user == null) return;

            var task = user.Tasks.Find(q => q.Id == idTask);
            if (task != null) user.Tasks.Remove(task);

            UserRepository.InsertOrUpdate(user);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void Delete(UserDto userDto)
        {
            var user = UserRepository.GetUser(userDto.Id);
            if (user == null) return;

            UserRepository.Remove(user);
            _unitOfWork.Save();
        }
        public static List<T> removeDuplicates<T>(List<T> list)
        {
            return new HashSet<T>(list).ToList();
        }
    }
}