using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalApp.Models.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(300)]
        public string Address { get; set; }

        [Required]
        public string MembershipCardNumber { get; set; }

        [Required]
        public DateTime MembershipCardValidityDate { get; set; }

        public DateTime? RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}