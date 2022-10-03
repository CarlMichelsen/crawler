using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class FailedUnknownRepository
{
    public static async Task<bool> AddFailedUnknown(DataContext context, FailedUnknownEntity failedUnknown)
    {
        try
        {
            if (context.FailedUnknownEntity is null) throw new NullReferenceException("Datacontext FailedUnknownEntity is null.");
            var exists = await context.FailedUnknownEntity.AnyAsync((f) => f.Id == failedUnknown.Id);
            if (exists) return true;
            await context.FailedUnknownEntity.AddAsync(failedUnknown);

            // save and exit
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}