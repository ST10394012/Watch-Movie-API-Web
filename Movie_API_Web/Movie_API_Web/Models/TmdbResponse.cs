using System.Text.Json.Serialization;
namespace Movie_API_Web.Models
{
    public class MovieResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = new List<int>();

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        
        public Movie ToMovie()
        {
            return new Movie
            {
                Id = this.Id,
                Title = this.Title,
                ReleaseDate = this.ReleaseDate,
                Overview = this.Overview,
                PosterPath = this.PosterPath,
                BackdropPath = this.BackdropPath,
                VoteAverage = this.VoteAverage,
                VoteCount = this.VoteCount,
                GenreIds = this.GenreIds,
                Popularity = this.Popularity,
                Adult = this.Adult,
                OriginalLanguage = this.OriginalLanguage
            };
        }
    }

    
    public class SearchResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<MovieResponse> Results { get; set; } = new List<MovieResponse>();

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}