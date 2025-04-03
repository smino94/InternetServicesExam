using System;

namespace MovieRentalApp.Models.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string MembershipCardNumber { get; set; }
        public DateTime MembershipCardValidityDate { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }

    public class ClientCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string MembershipCardNumber { get; set; }
        public DateTime MembershipCardValidityDate { get; set; }
        public int? MovieId { get; set; }
    }

    public class ClientUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string MembershipCardNumber { get; set; }
        public DateTime MembershipCardValidityDate { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? MovieId { get; set; }
    }
}