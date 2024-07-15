using APV._Plugins.WebAPI.Tmdb.Models;
using APV.CoreBusiness;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV._Plugins.WebAPI.Tmdb
{
    public static class TmdbMapper
    {
        static MapperConfiguration resultToMovieMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Result, Movie>());
        static Mapper resultToMovieMapper = new Mapper(resultToMovieMapperConfig);

        public static List<Movie> MapResultArrToMovieList(Result[] movieArr)
        {
            List<Movie> movieList = [];
            foreach (Result tmdbMovie in movieArr)
            {
                movieList.Add(resultToMovieMapper.Map<Movie>(tmdbMovie));
            }

            return movieList;
        }
    }
}
