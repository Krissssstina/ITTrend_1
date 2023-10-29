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
    public class SprintController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ISprintRepository SprintRepository => _unitOfWork.SprintRepository;

        public SprintController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IEnumerable<SprintDto> Get()
        {
            return SprintRepository.GetEntities().Adapt<IEnumerable<SprintDto>>();
        }

        [HttpGet]
        [Route("get-sprint")]
        [Authorize(Roles = "Admin,Manager")]
        public SprintDto GetSprint(int id)
        {
            return SprintRepository.GetSprint(id).Adapt<SprintDto>();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Post(SprintDto sprintDto)
        {
            var sprint = new Sprint
            {
                Name = sprintDto.Name,
                Description = sprintDto.Description,
                Comment = sprintDto.Comment,
                StartDt = sprintDto.StartDt,
                EndDt = sprintDto.EndDt,
                ProjectId = sprintDto.ProjectId
            };
            SprintRepository.Insert(sprint);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public void Put(SprintDto sprintDto)
        {
            var sprint = sprintDto.Id == 0 ? new Sprint() : SprintRepository.GetSprint(sprintDto.Id);
            if (sprint == null) return;
            sprint.Name = sprintDto.Name;
            sprint.Description = sprintDto.Description;
            sprint.Comment = sprintDto.Comment;
            sprint.StartDt = sprintDto.StartDt;
            sprint.EndDt = sprintDto.EndDt;
            sprint.ProjectId = sprintDto.ProjectId;

            SprintRepository.InsertOrUpdate(sprint);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void Delete(SprintDto sprintDto)
        {
            var sprint = SprintRepository.GetSprint(sprintDto.Id);
            if (sprint == null) return;

            SprintRepository.Remove(sprint);
            _unitOfWork.Save();
        }
    }
}
