using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class ProfileRepository
{
    private static UnknownEntity ToUnknown(UserEntity input)
    {
        return new UnknownEntity()
        {
            User = input,
            Recorded = DateTime.Now
        };
    }

    private static async Task<List<UserEntity>> FindNewUsers(List<UserEntity> allFriends, DataContext db)
    {
        if (db.UserEntity is null) return new List<UserEntity>();
        var exsisting = new List<UserEntity>();
        foreach (UserEntity friend in allFriends)
        {
            var found = await db.UserEntity.FirstOrDefaultAsync(ent => ent.Id == friend.Id);
            if (found is null) continue;
            exsisting.Add(found);
        }
        return allFriends.Where((f) => !exsisting.Any((e) => e?.Id == f.Id)).ToList();
    }

    private static async Task<List<UnknownEntity>> FindNewUnknownUsers(List<UserEntity> allUsers, DataContext db)
    {
        if (db.UnknownEntity is null) return new List<UnknownEntity>();
        var exsisting = new List<UnknownEntity>();
        foreach (UserEntity user in allUsers)
        {
            var found = await db.UnknownEntity.FirstOrDefaultAsync(unk => unk.User.Id == user.Id);
            if (found is null) continue;
            exsisting.Add(found);
        }
        return allUsers.Where((f) => !exsisting.Any((e) => e?.Id == f.Id)).Select(u => ToUnknown(u)).ToList();
    }

    public static async Task<bool> AddProfile(ProfileEntity profile)
    {
        try
        {
            using (var context = new DataContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                // only have new UserEntities in friends list
                profile.Friends = await FindNewUsers(profile.Friends, context); // this is a painpoint

                // add ProfileEntity to db
                if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
                context.ProfileEntity.Add(profile);

                // add new unknown entities to turn into profiles later
                if (context.UnknownEntity is null) throw new NullReferenceException("UnknownEntity datacontext is null");
                var unknownEntities = await FindNewUnknownUsers(profile.Friends, context);
                context.UnknownEntity.AddRange(unknownEntities);

                // save and exit
                context.SaveChanges();
                await dbContextTransaction.CommitAsync();
            }
            return true;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            return false;
        }
    }
}