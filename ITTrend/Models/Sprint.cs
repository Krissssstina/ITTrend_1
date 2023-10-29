namespace ITTrend.Models
{
    public class Sprint : EntityBase
    {
        public Sprint()
        {
            Tasks = new List<Task>();
        }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
        public List<Task> Tasks { get; set; }
        public int ProjectId { get;set; }
        public Project Project { get; set; }    



    }
}
