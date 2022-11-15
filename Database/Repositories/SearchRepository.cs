using Database.Entities;
using Database.Search;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class SearchRepository
{
    public static async Task<List<ProfileEntity>> NameSearch(DataContext context, string search)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var searchResult = from b in context.ProfileEntity where b.Username.ToLower().StartsWith(search.ToLower()) select b;
        var searchResultList = await searchResult
            .OrderBy(opt => opt.Username.Length - search.Length)
            .Take(10)
            .Include(opt => opt.RecentStats)
            .Include(opt => opt.Stats)
            .Include(opt => opt.OldUsernames)
            .Include(opt => opt.ProfileConnections)
            .Include(opt => opt.Friends)
            .ToListAsync();

        return searchResultList;
    }

    public static async Task<List<ProfileEntity>> FilterSearch(DataContext context, SearchFilter search, bool unlimited = false)
    {
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var clampedAmount = unlimited ? int.MaxValue : Math.Clamp(search.Amount, 0, 1000);
        var max = search?.Elo?.Max is not null ? search?.Elo?.Max : null;
        var min = search?.Elo?.Min is not null ? search?.Elo?.Min : null;
        var minFriends = search?.MinimumFriendAmount is not null ? search?.MinimumFriendAmount : null;
        var username = search?.Username is not null ? search.Username.ToLower() : null;

        return await context.ProfileEntity
            .Include(opt => opt.RecentStats)
            .Include(opt => opt.Stats)
            .Include(opt => opt.OldUsernames)
            .Include(opt => opt.Friends)
            .Where(pro => max == null || pro.Stats.Elo <= max)
            .Where(pro => min == null || pro.Stats.Elo >= min)
            .Where(pro => minFriends == null || pro.Friends.Count >= minFriends)
            .Where(pro => username == null || pro.Username.ToLower()
            .StartsWith(username))
            .OrderBy(opt => username != null ? opt.Username.Length - username.Length : 0)
            .OrderByDescending(pro => username == null ? pro.Friends.Count : 0)
            .Take(clampedAmount)
            .ToListAsync();
    }

    public static async Task<int> EloCountSearch(DataContext context, SearchRange range, bool includeDoubleZeroElo = false)
    {
        if (range.Max is null) throw new NullReferenceException("No max value in range search");
        if (range.Min is null) throw new NullReferenceException("No min value in range search");
        if (context.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        return await context.ProfileEntity
            .Include(p => p.Stats)
            .Where(p => p.Stats.Elo >= range.Min && p.Stats.Elo <= range.Max)
            .Where(p => !includeDoubleZeroElo || (p.Stats.Elo % 100) != 0)
            .CountAsync();
    }
}