namespace APV.Plugins.DataStore.WebAPI.Tmdb.Models
{
    public class MovieListQueryResponse
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
