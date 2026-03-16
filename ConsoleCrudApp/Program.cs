using MovieDiary.Console.Models;
using System;
using System.Collections.Generic;

namespace MovieDiarySimple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MovieRepository repo = new MovieRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать все фильмы");
                Console.WriteLine("2. Добавить фильм");
                Console.WriteLine("3. Изменить фильм");
                Console.WriteLine("4. Удалить фильм");
                Console.WriteLine("5. Найти фильм по ID");
                Console.WriteLine("0. Выход");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllMovies(repo);
                        break;
                    case "2":
                        AddMovie(repo);
                        break;
                    case "3":
                        UpdateMovie(repo);
                        break;
                    case "4":
                        DeleteMovie(repo);
                        break;
                    case "5":
                        FindMovieById(repo);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void ShowAllMovies(MovieRepository repo)
        {
            

            List<Movie> movies = repo.GetAllMovies();

            if (movies.Count == 0)
            {
                Console.WriteLine("Фильмов нет!");
                return;
            }

            Console.WriteLine("ID  Название                          Год");
            Console.WriteLine("----------------------------------------");

            foreach (Movie movie in movies)
            {
                string year = movie.ReleaseYear.HasValue ? movie.ReleaseYear.ToString() : "----";
                Console.WriteLine($"{movie.MovieID,-3} {movie.Title,-30} {year,-4}");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Всего: {movies.Count} фильмов");
        }

        static void AddMovie(MovieRepository repo)
        {
            Console.WriteLine("\nДобавление фильма\n");

            Movie movie = new Movie();

            Console.Write("Название: ");
            movie.Title = Console.ReadLine();

            Console.Write("Год выпуска: ");
            string yearInput = Console.ReadLine();
            if (int.TryParse(yearInput, out int year))
                movie.ReleaseYear = year;

            Console.Write("Длительность (мин): ");
            string durationInput = Console.ReadLine();
            if (int.TryParse(durationInput, out int duration))
                movie.DurationMinutes = duration;

            Console.Write("ID режиссера: ");
            string directorInput = Console.ReadLine();
            if (int.TryParse(directorInput, out int directorId))
                movie.DirectorID = directorId;

            repo.AddMovie(movie);
        }

        static void UpdateMovie(MovieRepository repo)
        {
            Console.WriteLine("\nИзменение фильма\n");

            Console.Write("Введите ID фильма: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            Movie movie = repo.GetMovieById(id);
            if (movie == null)
            {
                Console.WriteLine("Фильм не найден!");
                return;
            }

            Console.WriteLine($"Текущее название: {movie.Title}");
            Console.Write("Новое название: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
                movie.Title = title;

            Console.WriteLine($"Текущий год: {movie.ReleaseYear}");
            Console.Write("Новый год: ");
            string yearInput = Console.ReadLine();
            if (int.TryParse(yearInput, out int year))
                movie.ReleaseYear = year;

            repo.UpdateMovie(movie);
        }

        static void DeleteMovie(MovieRepository repo)
        {
            Console.WriteLine("\nУдаление фильма\n");

            Console.Write("Введите ID фильма: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            repo.DeleteMovie(id);
        }

        static void FindMovieById(MovieRepository repo)
        {
            Console.WriteLine("\nПоиск фильма\n");

            Console.Write("Введите ID фильма: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }

            Movie movie = repo.GetMovieById(id);
            if (movie == null)
            {
                Console.WriteLine("Фильм не найден!");
                return;
            }

            Console.WriteLine($"\nID: {movie.MovieID}");
            Console.WriteLine($"Название: {movie.Title}");
            Console.WriteLine($"Год: {movie.ReleaseYear}");
            Console.WriteLine($"Длительность: {movie.DurationMinutes} мин");
            Console.WriteLine($"ID режиссера: {movie.DirectorID}");
        }
    }
}
