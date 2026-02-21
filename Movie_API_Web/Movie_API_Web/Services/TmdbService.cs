using Movie_API_Web.Models;
using System.Text.Json;

namespace Movie_API_Web.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        
        private const string BASE_URL = "https://api.themoviedb.org/3";

        
        public const string IMAGE_BASE_URL = "https://image.tmdb.org/t/p/w500";

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            
            _apiKey = configuration["TmdbSettings:ApiKey"] ?? "YOUR_TMDB_API_KEY_HERE";
        }

        
        // Searches for movies by query string  
        public async Task<SearchResult> SearchMoviesAsync(string query, int page = 1)
        {
            try
            {
                
                string url = $"{BASE_URL}/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}&page={page}&language=en-US";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                
                SearchResponse searchResponse = JsonSerializer.Deserialize<SearchResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                //API to Application
                SearchResult result = new SearchResult
                {
                    Page = searchResponse.Page,
                    TotalResults = searchResponse.TotalResults,
                    TotalPages = searchResponse.TotalPages,
                    Results = searchResponse.Results.Select(r => r.ToMovie()).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching movies: {ex.Message}");
                return new SearchResult { Results = new List<Movie>() };
            }
        }

        
        // Gets popular movies currently trending
        public async Task<SearchResult> GetPopularMoviesAsync(int page = 1)
        {
            try
            {
                
                string url = $"{BASE_URL}/movie/popular?api_key={_apiKey}&page={page}&language=en-US";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                SearchResponse searchResponse = JsonSerializer.Deserialize<SearchResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                SearchResult result = new SearchResult
                {
                    Page = searchResponse.Page,
                    TotalResults = searchResponse.TotalResults,
                    TotalPages = searchResponse.TotalPages,
                    Results = searchResponse.Results.Select(r => r.ToMovie()).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching popular movies: {ex.Message}");
                return new SearchResult { Results = new List<Movie>() };
            }
        }

        
        // Gets movies now currently showing in theaters
        public async Task<SearchResult> GetNowPlayingMoviesAsync(int page = 1)
        {
            try
            {
                string url = $"{BASE_URL}/movie/now_playing?api_key={_apiKey}&page={page}&language=en-US";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                SearchResponse searchResponse = JsonSerializer.Deserialize<SearchResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                SearchResult result = new SearchResult
                {
                    Page = searchResponse.Page,
                    TotalResults = searchResponse.TotalResults,
                    TotalPages = searchResponse.TotalPages,
                    Results = searchResponse.Results.Select(r => r.ToMovie()).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching now playing movies: {ex.Message}");
                return new SearchResult { Results = new List<Movie>() };
            }
        }

        
        /// Gets upcoming movies coming soon to theaters
        public async Task<SearchResult> GetUpcomingMoviesAsync(int page = 1)
        {
            try
            {
                string url = $"{BASE_URL}/movie/upcoming?api_key={_apiKey}&page={page}&language=en-US";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                SearchResponse searchResponse = JsonSerializer.Deserialize<SearchResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                SearchResult result = new SearchResult
                {
                    Page = searchResponse.Page,
                    TotalResults = searchResponse.TotalResults,
                    TotalPages = searchResponse.TotalPages,
                    Results = searchResponse.Results.Select(r => r.ToMovie()).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching upcoming movies: {ex.Message}");
                return new SearchResult { Results = new List<Movie>() };
            }
        }
    }
}