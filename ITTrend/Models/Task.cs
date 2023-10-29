namespace ITTrend.Models
{
    public class Task : EntityBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<FileExtension> Files { get; set; } = new List<FileExtension>();

    }
}
