using SJHAFitness;
using SJHAFitness.Models;
using SQLite;

public static class DatabaseHelper
{
    private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appdb.db");

    public static void InitializeDatabase()
    {
        using (var db = new SQLiteConnection(dbPath))
        {

            db.CreateTable<Account>();

            db.CreateTable<Sessions>();
        }
    }

    public static void AddAccount(Account account)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            db.Insert(account);
        }
    }

    public static void AddSession(Sessions session)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            db.Insert(session);
        }
    }

    public static List<Sessions> GetSessions()
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            return db.Table<Sessions>().ToList();
        }
    }

    public static List<Sessions> GetSessionsByUser(int userID)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            return db.Table<Sessions>().Where(s => s.UserID == userID).ToList();
        }
    }

    public static void DeleteSession(Sessions sessions)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            db.Delete(sessions);
        }
    }

    public static Account GetAccountByEmail(string email)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            // Attempt to find the account with the specified email
            var account = db.Table<Account>().FirstOrDefault(a => a.Email == email);
            return account;
        }
    }

    public static bool SignupUser(string firstName, string lastName, string email, string password, int height, int weight, DateTime birthday)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            // existing user check
            var existingUser = db.Table<Account>().FirstOrDefault(a => a.Email == email);
            if (existingUser != null)
            {
                return false;
            }

            // Here maybe we hash the password

            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password, // maybe we look at hashing passwords.
                Email = email,
                Height = height,
                Weight = weight,
                Birthday = birthday
            };

            db.Insert(account);
            return true; // this conmfirms that signup was a success
        }
    }
    
}