namespace APV.CoreBusiness
{
    public class Movie
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; }
        public int[] Genre_ids { get; set; }
        public int Id { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public int Vote_count { get; set; }
        public string CarouselImage => $"https://image.tmdb.org/t/p/w780{Poster_path}";
        public string BackgroundImage => $"https://image.tmdb.org/t/p/original{Backdrop_path}";
        public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2{Poster_path ?? Backdrop_path}";
        public List<MovieCategory> Categories { get; set; }
    }
}
