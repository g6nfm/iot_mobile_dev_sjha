using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SJHAFitness
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        //public byte[] ProfilePicture { get; set; }

        [StringLength(256)]
        public string Password { get; set; } // Consider using a more secure way to store passwords

        public int? Height { get; set; }

        public int? Weight { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Birthday { get; set; }

        public int? MembershipTerm { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? MembershipStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? MembershipEndDate { get; set; }

        [StringLength(50)]
        public string MembershipName { get; set; }
    }
}