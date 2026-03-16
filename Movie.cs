using System;

namespace MovieDiary.Console.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }
        public int? DurationMinutes { get; set; }
        public int? DirectorID { get; set; }
    }
}