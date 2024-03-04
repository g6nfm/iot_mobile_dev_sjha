using SJHAFitness;
using SQLite;

public static class DatabaseHelper
{
    private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appdb.db");

    public static void InitializeDatabase()
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            db.CreateTable<Account>();
        }
    }

    public static void AddAccount(Account account)
    {
        using (var db = new SQLiteConnection(dbPath))
        {
            db.Insert(account);
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

    public static bool SignupUser(string email, string password, int height, int weight, DateTime birthday)
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
                //Name = name,
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