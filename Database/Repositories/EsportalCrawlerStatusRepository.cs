using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class EsportalCrawlerStatusRepository
{
    public static async Task<int> ProfileCount(DataContext db)
    {
        if (db.ProfileEntity is null) throw new NullReferenceException("ProfileEntity null");
        return await db.ProfileEntity.CountAsync();
    }

    public static async Task<int> UnknownCount(DataContext db)
    {
        if (db.UnknownEntity is null) throw new NullReferenceException("UnknownEntity null");
        return await db.UnknownEntity.CountAsync();
    }

    public static async Task<int> FailedUnknownCount(DataContext db)
    {
        if (db.FailedUnknownEntity is null) throw new NullReferenceException("FailedUnknownEntity null");
        return await db.FailedUnknownEntity.CountAsync();
    }

    public static async Task<int> SteamIdCount(DataContext db)
    {
        if (db.ProfileConnectionEntity is null) throw new NullReferenceException("ProfileConnectionEntity null");
        return await db.ProfileConnectionEntity.Where(c => c.SteamId64 != null).CountAsync();
    }
}