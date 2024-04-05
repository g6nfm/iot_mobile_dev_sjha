
using SQLite;

namespace SJHAFitness
{

    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Unique]
        public string Email { get; set; }

        public byte[] ProfilePicture { get; set; }
        public string Password { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        //NEW FIELDS
        public DateTime Birthday { get; set; }
        public int MembershipTerm { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public string MembershipName { get; set; }




    }
}
