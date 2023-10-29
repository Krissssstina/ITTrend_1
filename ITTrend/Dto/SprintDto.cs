using ITTrend.Models;

namespace ITTrend.Dto
{
    public class SprintDto:EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        
    }
}
