using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJHAFitness.Models
{
    public class Sessions
    {
        public DateTime Date {  get; set; }

        public TimeSpan Time { get; set; }

        public string Session {  get; set; }

        public string Focus { get; set; }

        public string Image {  get; set; }
    }
}
