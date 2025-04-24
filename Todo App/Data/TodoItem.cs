namespace Todo_App.Data
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UserId { get; set; } // Foreign key to User table
        // Foreign key to User table



    }
}
