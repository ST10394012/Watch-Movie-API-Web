namespace Movie_API_Web.Models
{
    public class SearchResult
    {
        public int Page { get; set; }

        public List<Movie> Results { get; set; } = new List<Movie>();

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }
    }
}
