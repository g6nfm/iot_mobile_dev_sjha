using Microsoft.EntityFrameworkCore;
using SJHAFitness.Models;
using SJHAFitness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class DatabaseHelper
{
    private static AppDbContext _context;

    public static void Initialize(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public static async Task InitializeDatabaseAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }
    public static async Task DropSessionsTableAsync()
    {
        var sessions = await _context.Sessions.ToListAsync();
        _context.Sessions.RemoveRange(sessions);
        await _context.SaveChangesAsync();
    }

    public static async Task DropAccountTableAsync()
    {
        var accounts = await _context.Account.ToListAsync();
        _context.Account.RemoveRange(accounts);
        await _context.SaveChangesAsync();
    }

    public static async Task AddAccountAsync(Account account)
    {
        await _context.Account.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public static async Task AddSessionAsync(Sessions session)
    {
        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public static async Task<List<Sessions>> GetSessionsAsync()
    {
        return await _context.Sessions.ToListAsync();
    }

    public static async Task<List<Sessions>> GetSessionsByUserAsync(int userID)
    {
        return await _context.Sessions.Where(s => s.UserID == userID).ToListAsync();
    }

    public static async Task DeleteSessionAsync(Sessions session)
    {
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
    }

    public static async Task<Account> GetAccountByEmailAsync(string email)
    {
        return await _context.Account.FirstOrDefaultAsync(a => a.Email == email);
    }

    public static async Task<bool> SignupUserAsync(string firstName, string lastName, string email, string password, int height, int weight, DateTime birthday, int membershipTerm,
        DateTime membershipStartDate, DateTime membershipEndDate, string membershipName)
    {

        // Ensure the context is not null
        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not initialized.");
        }

        
        var existingUser = await _context.Account.AnyAsync(a => a.Email == email);
        if (existingUser)
        {
            return false; // User already exists
        }
        

        var account = new Account
        {
            FirstName = firstName,
            LastName = lastName,
            Password = PasswordHasher.HashPassword(password), // Use a real password hashing method
            Email = email,
            Height = height,
            Weight = weight,
            Birthday = birthday,
            MembershipTerm = membershipTerm,
            MembershipStartDate = membershipStartDate,
            MembershipEndDate = membershipEndDate,
            MembershipName = membershipName
        };

        await _context.Account.AddAsync(account);
        await _context.SaveChangesAsync();
        return true; // Signup was successful
    }

    public static async Task<bool> UpdatePasswordAsync(Account account, string newPassword)
    {
        var existingAccount = await _context.Account.FirstOrDefaultAsync(a => a.UserID == account.UserID);
        if (existingAccount != null)
        {
            existingAccount.Password = PasswordHasher.HashPassword(newPassword); // Use a real password hashing method
            _context.Account.Update(existingAccount);
            await _context.SaveChangesAsync();
            return true; // Password update was successful
        }

        return false; // Account not found
    }

    public static async Task<bool> UpdatePasswordByEmailAsync(string email, string newPassword)
    {
        var existingAccount = await _context.Account.FirstOrDefaultAsync(a => a.Email == email);
        if (existingAccount != null)
        {
            existingAccount.Password = PasswordHasher.HashPassword(newPassword); // Use a real password hashing method
            _context.Account.Update(existingAccount);
            await _context.SaveChangesAsync();
            return true; // Password update was successful
        }

        return false; // Account not found
    }

    public static async Task<bool> UpdateAccountAsync(Account account)
    {
        _context.Account.Update(account);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public static async Task<bool> ChangeMembershipAsync(int userId, int newMembershipTerm, string newMembershipName)
    {
        var account = await _context.Account.FirstOrDefaultAsync(a => a.UserID == userId);
        if (account != null)
        {
            account.MembershipTerm = newMembershipTerm;
            account.MembershipStartDate = DateTime.Now;
            if (account.MembershipStartDate.HasValue)
            {
                account.MembershipEndDate = account.MembershipStartDate.Value.AddMonths(newMembershipTerm);
            }
            else
            {
                return false;
            }
            account.MembershipName = newMembershipName;
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        return false; // Account not found
    }

    public static async Task<bool> CancelMembershipAsync(int userId)
    {
        var account = await _context.Account.FirstOrDefaultAsync(a => a.UserID == userId);

        if (account != null)
        {
            // Set the membership details to reflect cancellation
            account.MembershipName = "N/A"; // or null, if that's preferred
            account.MembershipTerm = 0;

            // You may also want to set the start date to the current date
            // if that's how you want to indicate a canceled membership
            account.MembershipStartDate = DateTime.Now;
            account.MembershipEndDate = DateTime.Now; // Reflects the membership has ended

            // If you're using EF Core, it tracks changes so this might be optional
            _context.Account.Update(account);

            await _context.SaveChangesAsync();
            return true;
        }

        return false; // Account not found
    }
}