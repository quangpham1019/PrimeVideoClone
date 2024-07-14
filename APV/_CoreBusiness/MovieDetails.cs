using APV._Plugins.WebAPI.Tmdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV._CoreBusiness
{
    public class MovieDetails
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; }
        public object Belongs_to_collection { get; set; }
        public int Budget { get; set; }
        public List<Genre> Genres { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public string Imdb_id { get; set; }
        public string[] Origin_country { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public float Popularity { get; set; }
        public string Poster_path { get; set; }
        public ProductionCompany[] Production_companies { get; set; }
        public ProductionCountry[] Production_countries { get; set; }
        public string ReleaseDate { get; set; }
        public int Revenue { get; set; }
        public int Runtime { get; set; }
        public SpokenLanguage[] Spoken_languages { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Title { get; set; }
        public float Vote_average { get; set; }
        public int Vote_count { get; set; }
    }
}
