using SQLite;

namespace SJHAFitness
{

    public class Membership
    {
        [PrimaryKey, AutoIncrement]
       public int MembershipId { get; set; }
        public int UserID { get; set; }

        public string MembershipType { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }

    }
}