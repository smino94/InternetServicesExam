using System.Collections.Generic;

namespace MovieRentalApp.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public decimal BoxOfficeEarnings { get; set; }
    }

    public class MovieCreateDto
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public decimal BoxOfficeEarnings { get; set; }
    }

    public class MovieUpdateDto
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public decimal BoxOfficeEarnings { get; set; }
    }
}