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
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IProjectRepository ProjectRepository => _unitOfWork.ProjectRepository;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<ProjectDto> Get()
        {
            return ProjectRepository.GetEntities().Adapt<IEnumerable<ProjectDto>>();
        }

        [HttpGet]
        [Route("get-project")]
        [Authorize]
        public ProjectDto GetProject(int id)
        {
            return ProjectRepository.GetProject(id).Adapt<ProjectDto>();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Post(ProjectDto projectDto)
        {
            var project = new Project 
            { 
                Name = projectDto.Name,
                Description= projectDto.Description
            };
            ProjectRepository.Insert(project);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Put(ProjectDto projectDto)
        {
            var project = projectDto.Id == 0 ? new Project() : ProjectRepository.GetProject(projectDto.Id);
            if (project == null) return;
            project.Name = projectDto.Name;

            ProjectRepository.InsertOrUpdate(project);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Delete(ProjectDto projectDto)
        {
            var project = ProjectRepository.GetProject(projectDto.Id);
            if (project == null) return;
            
            ProjectRepository.Remove(project);
            _unitOfWork.Save();
        }
    }
}
