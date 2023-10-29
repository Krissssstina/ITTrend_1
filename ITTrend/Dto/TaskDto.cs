using ITTrend.Models;

namespace ITTrend.Dto
{
    public class TaskDto:EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
