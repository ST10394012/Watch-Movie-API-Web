using Movie_API_Web.Models;
using System.Text.Json;

namespace Movie_API_Web.Services
{
    public class CommentService
    {

        private readonly string _filePath;

        public CommentService()
        {
            
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "comments.json");

            // Ensures Data directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

            // Create file if it doesn't exist
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        
        /// Gets all comments for a specific movie
        public List<Comment> GetCommentsByMovieId(int movieId)
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                List<Comment> allComments = JsonSerializer.Deserialize<List<Comment>>(json) ?? new List<Comment>();

                // Filter comments for the specific movie
                return allComments.Where(c => c.MovieId == movieId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading comments: {ex.Message}");
                return new List<Comment>();
            }
        }

        
        // Adds a new comment to a movie
        
        public bool AddComment(Comment comment)
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                List<Comment> allComments = JsonSerializer.Deserialize<List<Comment>>(json) ?? new List<Comment>();

                // Add the new comment
                comment.Id = Guid.NewGuid().ToString();
                comment.CreatedAt = DateTime.UtcNow;
                allComments.Add(comment);

                // Write back to file
                var options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJson = JsonSerializer.Serialize(allComments, options);
                File.WriteAllText(_filePath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding comment: {ex.Message}");
                return false;
            }
        }

        
        /// Adds a reply to an existing comment
        public bool AddReply(int movieId, string commentId, Reply reply)
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                List<Comment> allComments = JsonSerializer.Deserialize<List<Comment>>(json) ?? new List<Comment>();

                
                Comment targetComment = allComments.FirstOrDefault(c => c.MovieId == movieId && c.Id == commentId);
                if (targetComment == null)
                {
                    return false;
                }

                
                reply.Id = Guid.NewGuid().ToString();
                reply.CreatedAt = DateTime.UtcNow;
                targetComment.Replies.Add(reply);

                
                var options = new JsonSerializerOptions { WriteIndented = true };
                string updatedJson = JsonSerializer.Serialize(allComments, options);
                File.WriteAllText(_filePath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding reply: {ex.Message}");
                return false;
            }
        }

        
        // Deletes a comment by ID
        public bool DeleteComment(int movieId, string commentId)
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                List<Comment> allComments = JsonSerializer.Deserialize<List<Comment>>(json) ?? new List<Comment>();

                
                Comment commentToRemove = allComments.FirstOrDefault(c => c.MovieId == movieId && c.Id == commentId);
                if (commentToRemove != null)
                {
                    allComments.Remove(commentToRemove);

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string updatedJson = JsonSerializer.Serialize(allComments, options);
                    File.WriteAllText(_filePath, updatedJson);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting comment: {ex.Message}");
                return false;
            }
        }
    }
}
