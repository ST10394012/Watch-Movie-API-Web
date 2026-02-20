namespace Movie_API_Web.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

       
        public string ReleaseDate { get; set; }

        
        public string Overview { get; set; }

        
        public string PosterPath { get; set; }

        
        public string BackdropPath { get; set; }

        
        public double VoteAverage { get; set; }

       
        public int VoteCount { get; set; }

        
        public List<int> GenreIds { get; set; } = new List<int>();

       
        public double Popularity { get; set; }

        
        public bool Adult { get; set; }

        
        public string OriginalLanguage { get; set; }
    }
}
