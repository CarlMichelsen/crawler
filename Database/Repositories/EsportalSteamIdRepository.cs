using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class EsportalSteamIdRepository
{
    private static async Task UpsertSteamIdTransaction(ulong userId, string? steamId)
    {
        using var context = new DataContext();
        using var dbContextTransaction = context.Database.BeginTransaction();
        if (context.ProfileEntity is null) throw new InvalidOperationException("Invalid ProfileEntity DataContext.");
        var profile = context.ProfileEntity
            .Where(pro => pro.Id == userId)
            .Include(pro => pro.ProfileConnections)
            .FirstOrDefault();
        if (profile is null) throw new Exception($"No profile with userId {userId} found!");

        if (profile.ProfileConnections is null)
        {
            var conn = new ProfileConnectionEntity
            {
                SteamId64 = steamId
            };
            profile.ProfileConnections = conn;
        }
        else
        {
            profile.ProfileConnections.SteamId64 = steamId;
        }

        context.SaveChanges();
        await dbContextTransaction.CommitAsync();
    }

    public static async Task<ProfileEntity?> GetNextSteamIdCandidate(DataContext db)
    {
        if (db.ProfileEntity is null) throw new InvalidOperationException("Invalid ProfileEntity DataContext.");
        return await db.ProfileEntity
            .Include(pro => pro.ProfileConnections)
            .Where(pro => pro.ProfileConnections == null)
            .OrderByDescending(pro => pro.Friends.Count)
            .FirstOrDefaultAsync();
    }

    public static async Task<bool> UpsertSteamId(ulong userId, string? steamId)
    {
        try
        {
            await UpsertSteamIdTransaction(userId, steamId);
        }
        catch (System.Exception)
        {
            return false;
        }
        return true;
    }
}