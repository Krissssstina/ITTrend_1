namespace ITTrend.Models
{
    public class FileExtension:EntityBase
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string FilePath { get; set;  }


    }
}
