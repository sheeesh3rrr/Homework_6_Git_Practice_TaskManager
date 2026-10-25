namespace TaskTracker.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public TaskItem(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = TaskStatus.New;
            Priority = TaskPriority.Medium;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} | {Status} | {Priority}";
        }
    }
}
