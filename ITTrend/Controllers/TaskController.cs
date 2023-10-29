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
    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ITaskRepository TaskRepository => _unitOfWork.TaskRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _webHostEnvironment;

        public TaskController(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Hosting.IHostingEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IEnumerable<TaskDto> Get()
        {
            return TaskRepository.GetEntities().Adapt<IEnumerable<TaskDto>>();
        }

        [HttpGet]
        [Route("get-task")]
        [Authorize(Roles = "Admin,Manager")]
        public TaskDto GetTask(int id)
        {
            return TaskRepository.GetTask(id).Adapt<TaskDto>();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,CreateObject")]
        public void Post(TaskDto taskDto)
        {
            var task = new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Comment = taskDto.Comment,
                SprintId = taskDto.SprintId,
                UserId = taskDto.UserId

            };
            if (task.UserId == null) task.UserId = 1;
            TaskRepository.Insert(task);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public void Put(TaskDto taskDto)
        {
            var task = taskDto.Id == 0 ? new Models.Task() : TaskRepository.GetTask(taskDto.Id);
            if (task == null) return;
            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.Comment = taskDto.Comment;
            task.SprintId = taskDto.SprintId;
            task.UserId = taskDto.UserId;

            TaskRepository.InsertOrUpdate(task);
            _unitOfWork.Save();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void Delete(TaskDto taskDto)
        {
            var task = TaskRepository.GetTask(taskDto.Id);
            if (task == null) return;

            TaskRepository.Remove(task);
            _unitOfWork.Save();
        }

        [HttpPost]
        [Route("add-task-file")]
        [Authorize]
        public void AddFile([FromForm] TaskFileDto dto)
        {
            var task = TaskRepository.GetTask(dto.Id);
            var webPath = Path.Combine(_webHostEnvironment.WebRootPath, "task");

            if (!Directory.Exists(webPath))
            {
                Directory.CreateDirectory(webPath);
            }

            var fileName = Path.GetFileName(dto.File.FileName);
            var filePath = Path.Combine(webPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            dto.File.CopyToAsync(stream);

            var fileExt = new FileExtension();
            fileExt.TaskId = task.Id;
            fileExt.FilePath = Path.Combine("task", fileName);

            task.Files.Add(fileExt);
            TaskRepository.Update(task);
            _unitOfWork.Save();
        }

        [HttpGet]
        [Route("get-task-files/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetFiles(int id)
        {
            var task = TaskRepository.GetTask(id);

            var files = new List<byte[]>();

            foreach (var file in task.Files)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, file.FilePath);
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
              //  files.Add(new FileContentResult(fileBytes, "application/octet-stream"));
              files.Add(fileBytes);
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("get-userTasks")]
        [Authorize(Roles = "Admin,Manager")]
        public IEnumerable<TaskDto> GetUserTasks(int id)
        {
            return TaskRepository.GetEntities().Adapt<IEnumerable<TaskDto>>().Where(q=>q.UserId==id);
        }
    }
}
