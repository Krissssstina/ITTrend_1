namespace ITTrend.Models
{
    public class Project:EntityBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Sprint> Sprints { get; set; }
    }
}
