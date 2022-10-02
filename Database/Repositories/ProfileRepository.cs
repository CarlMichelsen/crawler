using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class ProfileRepository
{
    public static async Task<bool> AddProfileTransaction(DataContext context, ProfileEntity profile)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        if (context.UserEntity is null) throw new NullReferenceException("UserEntity datacontext is null");
        if (context.UnknownEntity is null) throw new NullReferenceException("UnknownEntity datacontext is null");

        try
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                var allFriends = new List<UserEntity>(profile.Friends);
                var exsistingFriends = new List<UserEntity>();
                profile.Friends = new List<UserEntity>();

                foreach (var f in allFriends)
                {
                    var exsisting = context.UserEntity.Where(e => e.Id == f.Id).FirstOrDefault();
                    if (exsisting is not null) exsistingFriends.Add(exsisting);
                }
                var newFriends = allFriends.Where(a => context.UserEntity.FirstOrDefault(e => e.Id == a.Id) == null);

                // add ProfileEntity to db
                profile.Recorded = DateTime.Now;
                context.ProfileEntity.Add(profile);

                // add new friends
                if (newFriends.Any())
                {
                    context.UserEntity.AddRange(newFriends);
                    var unknownEntities = newFriends.Select(f => new UnknownEntity{ User = f, Recorded = DateTime.Now });
                    context.UnknownEntity.AddRange(unknownEntities);
                }

                // update exsisting friends
                if (exsistingFriends.Any())
                {
                    context.UserEntity.UpdateRange(exsistingFriends);
                }
                
                context.SaveChanges();
                await dbContextTransaction.CommitAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public static async Task<bool> UpdateProfileTransaction(DataContext context, ProfileEntity profile)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        if (context.UserEntity is null) throw new NullReferenceException("UserEntity datacontext is null");
        if (context.UnknownEntity is null) throw new NullReferenceException("UnknownEntity datacontext is null");

        try
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                var allFriends = new List<UserEntity>(profile.Friends);
                var exsistingFriends = new List<UserEntity>();
                profile.Friends = new List<UserEntity>();

                foreach (var f in allFriends)
                {
                    var exsisting = context.UserEntity.Where(e => e.Id == f.Id).FirstOrDefault();
                    if (exsisting is not null) exsistingFriends.Add(exsisting);
                }
                var newFriends = allFriends.Where(a => context.UserEntity.FirstOrDefault(e => e.Id == a.Id) == null);

                // add ProfileEntity to db
                profile.Recorded = DateTime.Now;
                context.ProfileEntity.Update(profile);

                // add never seen before by system friends
                if (newFriends.Any())
                {
                    context.UserEntity.AddRange(newFriends);
                    var unknownEntities = newFriends.Select(f => new UnknownEntity{ User = f, Recorded = DateTime.Now });
                    context.UnknownEntity.AddRange(unknownEntities);
                }

                // update exsisting friends
                if (exsistingFriends.Any())
                {
                    context.UserEntity.UpdateRange(exsistingFriends);
                }
                
                context.SaveChanges();
                await dbContextTransaction.CommitAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public static async Task<ProfileEntity?> GetProfileById(DataContext context, ulong? userId)
    {
        if (userId is null) return null;
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        return await context.ProfileEntity
            .Where(p => p.Id == userId)
            .Include(p => p.ProfileConnections)
            .Include(p => p.Stats)
            .Include(p => p.RecentStats)
            .Include(p => p.Friends)
            .Include(p => p.OldUsernames)
            .FirstOrDefaultAsync();
    }

    public static async Task<ProfileEntity?> GetNextOutDated(DataContext context, TimeSpan timeToLive)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var cutoff = DateTime.Now.Subtract(timeToLive);
        var outdated = await context.ProfileEntity
            .Where(p => p.Recorded.CompareTo(cutoff)<0)
            .OrderByDescending(p => p.Recorded)
            .FirstOrDefaultAsync();
        
        Console.WriteLine($"Outdated recorded at: {outdated?.Recorded.ToShortDateString()}");
        return outdated;
    }
}