using System;
using System.Collections.Generic;
using System.Linq;
using MovieDiary.Console.Models;

namespace MovieDiary.Console.EF
{
    public class EfMovieRepository
    {
        private readonly MovieContext _context;

        public EfMovieRepository()
        {
            _context = new MovieContext();
        }


        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            System.Console.WriteLine("Фильм добавлен через EF!");
        }

        public void UpdateMovie(Movie movie)
        {
            var existing = _context.Movies.Find(movie.MovieID);
            if (existing != null)
            {
                existing.Title = movie.Title;
                existing.ReleaseYear = movie.ReleaseYear;
                existing.DurationMinutes = movie.DurationMinutes;
                existing.DirectorID = movie.DirectorID;
                _context.SaveChanges();
                System.Console.WriteLine("Фильм обновлен через EF!");
            }
            else
            {
                System.Console.WriteLine($"Фильм с ID {movie.MovieID} не найден!");
            }
        }
        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                System.Console.WriteLine($"Фильм с ID {id} удален через EF!");
            }
            else
            {
                System.Console.WriteLine($"Фильм с ID {id} не найден!");
            }
        }
        public Movie GetMovieById(int id)
        {
            return _context.Movies.Find(id);
        }
    }
}
