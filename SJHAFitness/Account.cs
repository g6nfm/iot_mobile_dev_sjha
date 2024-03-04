
using SQLite;

namespace SJHAFitness
{

    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Unique]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Birthday { get; set; }

    }
}
