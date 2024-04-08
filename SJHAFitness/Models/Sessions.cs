using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SJHAFitness.Models
{
    public class Sessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionID { get; set; }

        [ForeignKey("Account")]
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Session { get; set; }
        public string Focus { get; set; }
        public string Image { get; set; } // Ensure you have a strategy to handle image storage. In Azure, consider using Blob Storage.

        // Navigation property for Entity Framework Core
        public virtual Account User { get; set; }
    }
}