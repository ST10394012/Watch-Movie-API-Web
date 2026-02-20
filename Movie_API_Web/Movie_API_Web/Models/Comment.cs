namespace Movie_API_Web.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        
        public int MovieId { get; set; }

        
        public string Author { get; set; }

        
        public string Email { get; set; }

        
        public string Message { get; set; }

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public List<Reply> Replies { get; set; } = new List<Reply>();
    }

    public class Reply
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();

        
        public string Author { get; set; }

        
        public string Email { get; set; }

        
        public string Message { get; set; }

        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
