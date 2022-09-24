using Database.Entities;
using Database.Search;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class SearchRepository
{
    public static async Task<List<ProfileEntity>> NameSearch(string search, DataContext db)
    {
        if (db.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var searchResult = from b in db.ProfileEntity where b.Username.ToLower().StartsWith(search.ToLower()) select b;
        var searchResultList = await searchResult
            .OrderBy(opt => opt.Username.Length-search.Length)
            .Take(10)
            .Include(opt => opt.RecentStats)
            .Include(opt => opt.Stats)
            .Include(opt => opt.OldUsernames)
            .Include(opt => opt.Friends)
            .ToListAsync();
        return searchResultList;
    }

    public static async Task<List<ProfileEntity>> FilterSearch(SearchFilter search, DataContext db)
    {
        if (db.ProfileEntity is null) throw new NullReferenceException("ProfileEntity datacontext is null");
        var clampedAmount = Math.Clamp(search.Amount, 0, 1000);
        var max = search?.Elo?.Max is not null ? search?.Elo?.Max : null;
        var min = search?.Elo?.Min is not null ? search?.Elo?.Min : null;
        var minFriends = search?.MinimumFriendAmount is not null ? search?.MinimumFriendAmount : null;
        var username = search?.Username is not null ? search.Username.ToLower() : null;

        return await db.ProfileEntity
            .Include(opt => opt.RecentStats)
            .Include(opt => opt.Stats)
            .Include(opt => opt.OldUsernames)
            .Include(opt => opt.Friends)
            .Where(pro => max != null ? pro.Stats.Elo <= max : true)
            .Where(pro => min != null ? pro.Stats.Elo >= min : true)
            .Where(pro => minFriends != null ? pro.Friends.Count() >= minFriends : true)
            .Where(pro => username != null ? pro.Username.ToLower().StartsWith(username) : true)
            .OrderBy(opt => username != null ? opt.Username.Length-username.Length : 0)
            .Take(clampedAmount)
            .ToListAsync();
    }
}