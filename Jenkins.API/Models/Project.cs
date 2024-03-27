namespace Jenkins.API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public int OwnerId { get; set; }
        public bool Shared { get; set; }
    }
}
