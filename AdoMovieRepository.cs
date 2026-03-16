using Microsoft.Data.SqlClient;
using MovieDiary.Console.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MovieDiarySimple
{
    public class MovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MovieDiary"].ConnectionString;
        }

        // Получить все фильмы
        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT MovieID, Title, ReleaseYear, DurationMinutes, DirectorID FROM Movies";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Movie movie = new Movie();

                    movie.MovieID = Convert.ToInt32(reader["MovieID"]);
                    movie.Title = reader["Title"].ToString();

                    if (reader["ReleaseYear"] != DBNull.Value)
                        movie.ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]);

                    if (reader["DurationMinutes"] != DBNull.Value)
                        movie.DurationMinutes = Convert.ToInt32(reader["DurationMinutes"]);

                    if (reader["DirectorID"] != DBNull.Value)
                        movie.DirectorID = Convert.ToInt32(reader["DirectorID"]);

                    movies.Add(movie);
                }
            }

            return movies;
        }

        // Добавить фильм
        public void AddMovie(Movie movie)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO Movies (Title, ReleaseYear, DurationMinutes, DirectorID) " +
                             "VALUES (@Title, @ReleaseYear, @DurationMinutes, @DirectorID)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Title", movie.Title ?? "");
                cmd.Parameters.AddWithValue("@ReleaseYear", (object)movie.ReleaseYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DurationMinutes", (object)movie.DurationMinutes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DirectorID", (object)movie.DirectorID ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Фильм добавлен!");
            }
        }

        // Обновить фильм
        public void UpdateMovie(Movie movie)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "UPDATE Movies SET Title = @Title, ReleaseYear = @ReleaseYear, " +
                             "DurationMinutes = @DurationMinutes, DirectorID = @DirectorID " +
                             "WHERE MovieID = @MovieID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MovieID", movie.MovieID);
                cmd.Parameters.AddWithValue("@Title", movie.Title ?? "");
                cmd.Parameters.AddWithValue("@ReleaseYear", (object)movie.ReleaseYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DurationMinutes", (object)movie.DurationMinutes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DirectorID", (object)movie.DirectorID ?? DBNull.Value);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Фильм обновлен!");
                else
                    Console.WriteLine("Фильм не найден!");
            }
        }

        // Удалить фильм
        public void DeleteMovie(int movieId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Movies WHERE MovieID = @MovieID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MovieID", movieId);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("Фильм удален!");
                else
                    Console.WriteLine("Фильм не найден!");
            }
        }

        // Найти фильм по ID
        public Movie GetMovieById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT MovieID, Title, ReleaseYear, DurationMinutes, DirectorID FROM Movies WHERE MovieID = @MovieID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MovieID", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Movie movie = new Movie();
                    movie.MovieID = Convert.ToInt32(reader["MovieID"]);
                    movie.Title = reader["Title"].ToString();

                    if (reader["ReleaseYear"] != DBNull.Value)
                        movie.ReleaseYear = Convert.ToInt32(reader["ReleaseYear"]);

                    if (reader["DurationMinutes"] != DBNull.Value)
                        movie.DurationMinutes = Convert.ToInt32(reader["DurationMinutes"]);

                    if (reader["DirectorID"] != DBNull.Value)
                        movie.DirectorID = Convert.ToInt32(reader["DirectorID"]);

                    return movie;
                }

                return null;
            }
        }
    }
}