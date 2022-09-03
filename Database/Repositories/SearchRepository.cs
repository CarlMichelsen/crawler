using Database.Entities;
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
}