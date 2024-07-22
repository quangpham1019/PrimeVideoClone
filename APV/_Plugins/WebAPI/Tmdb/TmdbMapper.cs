using APV._Plugins.WebAPI.Tmdb.Models;
using APV.CoreBusiness;
using AutoMapper;

namespace APV._Plugins.WebAPI.Tmdb
{
    public static class TmdbMapper
    {
        static readonly MapperConfiguration resultToMovieMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Result, Movie>());
        static readonly Mapper resultToMovieMapper = new Mapper(resultToMovieMapperConfig);

        static readonly Mapper genreMapper = new Mapper(
            new MapperConfiguration(cfg => cfg.CreateMap<Models.Genre, CoreBusiness.Genre>()));
        static readonly Mapper productionCompanyMapper = new Mapper(
            new MapperConfiguration(cfg => cfg.CreateMap<Models.ProductionCompany, CoreBusiness.ProductionCompany>()));
        static readonly Mapper productionCountryMapper = new Mapper(
            new MapperConfiguration(cfg => cfg.CreateMap<Models.ProductionCountry, CoreBusiness.ProductionCountry>()));
        static readonly Mapper spokenLanguageMapper = new Mapper(
            new MapperConfiguration(cfg => cfg.CreateMap<Models.SpokenLanguage, CoreBusiness.SpokenLanguage>()));

        static readonly MapperConfiguration tmdbMovieDetailsToMovieDetailsMapperConfig =
            new MapperConfiguration(
                cfg => cfg
                .CreateMap<TmdbMovieDetails, MovieDetails>()
                .ForMember(
                    movieDetail => movieDetail.Genres,
                    opt => opt.MapFrom(
                        curTmdbMovieDetails => MapSomething<Models.Genre, CoreBusiness.Genre>(curTmdbMovieDetails.Genres, genreMapper))
                )
                .ForMember(
                    movieDetail => movieDetail.Production_companies,
                    opt => opt.MapFrom(
                        curTmdbMovieDetails => MapSomething<Models.ProductionCompany, CoreBusiness.ProductionCompany>(curTmdbMovieDetails.Production_companies, productionCompanyMapper))
                )
                .ForMember(
                    movieDetail => movieDetail.Production_countries,
                    opt => opt.MapFrom(
                        curTmdbMovieDetails => MapSomething<Models.ProductionCountry, CoreBusiness.ProductionCountry>(curTmdbMovieDetails.Production_countries, productionCountryMapper))
                ).ForMember(
                    movieDetail => movieDetail.Spoken_languages,
                    opt => opt.MapFrom(curTmdbMovieDetails => MapSomething<Models.SpokenLanguage, CoreBusiness.SpokenLanguage>(curTmdbMovieDetails.Spoken_languages, spokenLanguageMapper))
                )
            );
        static readonly Mapper tmdbMovieDetailsToMovieDetailsMapper = new Mapper(tmdbMovieDetailsToMovieDetailsMapperConfig);

        public static List<Movie> MapResultArrToMovieList(Result[] movieArr)
        {
            List<Movie> movieList = [];
            foreach (Result tmdbMovie in movieArr)
            {
                movieList.Add(resultToMovieMapper.Map<Movie>(tmdbMovie));
            }

            return movieList;
        }

        public static MovieDetails MapTmdbMovieDetailsToMovieDetails(TmdbMovieDetails tmdbMovieDetails)
        {
            return tmdbMovieDetailsToMovieDetailsMapper.Map<MovieDetails>(tmdbMovieDetails);
        }

        static List<T> MapSomething<U, T>(IEnumerable<U> mapFromList, Mapper mapper)
        {
            List<T> resultList = [];
            foreach (U mapFromListItem in mapFromList)
            {
                resultList.Add(mapper.Map<T>(mapFromListItem));
            }

            return resultList;
        }
    }
}
