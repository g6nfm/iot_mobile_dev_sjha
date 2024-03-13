using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SJHAFitness.Models
{
    public class Sessions
    {
        [PrimaryKey, AutoIncrement]
        public int SessionID { get; set; }

        public int UserID { get; set; }

        public DateTime Date {  get; set; }

        public TimeSpan Time { get; set; }

        public string Session {  get; set; }

        public string Focus { get; set; }

        public string Image {  get; set; }
    }
}
