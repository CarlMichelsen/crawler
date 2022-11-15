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
            // handle friends separately
            var allFriends = new List<UserEntity>(profile.Friends);
            var exsistingFriends = new List<UserEntity>();
            var newFriends = new List<UserEntity>();
            profile.Friends = new List<UserEntity>();

            foreach (var f in allFriends)
            {
                var exsisting = await context.UserEntity.Where(e => e.Id == f.Id).FirstOrDefaultAsync();
                if (exsisting is not null) exsistingFriends.Add(exsisting);
                else if (exsisting is null) newFriends.Add(f);
            }

            // add ProfileEntity to db
            profile.Recorded = DateTime.Now;
            context.ProfileEntity.Add(profile);

            // add new friends
            if (newFriends.Any())
            {
                context.UserEntity.AddRange(newFriends);
                var unknownEntities = newFriends.Select(f => new UnknownEntity { User = f, Recorded = DateTime.Now });
                context.UnknownEntity.AddRange(unknownEntities);
            }

            // update exsisting friends
            if (exsistingFriends.Any())
            {
                context.UserEntity.UpdateRange(exsistingFriends);
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"AddProfileTransaction -> {e.Message}");
            return false;
        }
    }

    public static async Task<bool> UpdateProfileTransaction(DataContext context, ProfileEntity profileInput)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        if (context.UserEntity is null) throw new NullReferenceException("UserEntity datacontext is null");
        if (context.UnknownEntity is null) throw new NullReferenceException("UnknownEntity datacontext is null");

        try
        {
            var profile = await context.ProfileEntity
                .Include(p => p.ProfileConnections)
                .Include(p => p.Stats)
                .Include(p => p.RecentStats)
                .Include(p => p.Friends)
                .Include(p => p.OldUsernames)
                .FirstOrDefaultAsync(p => p.Id == profileInput.Id);

            var oldDate = profile?.Recorded;
            if (profile is null) throw new Exception("Could not find \"exsisting\" profile entity in database.");
            context.ProfileEntity.Attach(profile);
            profile.Recorded = DateTime.Now;

            var friendsInStringFormat = profile.Friends.Select(f => $"{f.Username}<{f.Id}>");
            Console.WriteLine($"USER|{profile.Username}<{profile.Id}>| --> FRIENDS|{string.Join(",", friendsInStringFormat)}|");

            await context.SaveChangesAsync();
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
            .Where(p => p.Recorded.CompareTo(cutoff) < 0)
            .OrderBy(p => p.Recorded)
            .FirstOrDefaultAsync();

        return outdated;
    }

    public static async Task<bool> SetRecordedDate(DataContext context, ulong userId, DateTime dateTime)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var profile = context.ProfileEntity.FirstOrDefault(p => p.Id == userId);
        if (profile is not null)
        {
            profile.Recorded = dateTime;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}