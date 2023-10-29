using ITTrend.Dto;
using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.CustomRepository.Repository;
using ITTrend.RepositoryPattern.UnitOfWork;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITTrend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRoleRepository RoleRepository => _unitOfWork.RoleRepository;

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<RoleDto> Get()
        {
            return RoleRepository.GetEntities().Adapt<IEnumerable<RoleDto>>();
        }

        [HttpGet]
        [Route("get-role")]
        [Authorize(Roles = "Admin")]
        public RoleDto GetRole(int id)
        {
            return RoleRepository.GetRole(id).Adapt<RoleDto>();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post(RoleDto roleDto)
        {
            var role = new Role { Name = roleDto.Name };
            RoleRepository.Insert(role);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public void Put(RoleDto roleDto)
        {
            var role = roleDto.Id == 0 ? new Role() : RoleRepository.GetRole(roleDto.Id);
            if (role == null) return;
            role.Name = roleDto.Name;

            RoleRepository.InsertOrUpdate(role);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void Delete(RoleDto roleDto)
        {
            var role = RoleRepository.GetRole(roleDto.Id);
            if (role == null) return;

            RoleRepository.Remove(role);
            _unitOfWork.Save();
        }
    }
}
