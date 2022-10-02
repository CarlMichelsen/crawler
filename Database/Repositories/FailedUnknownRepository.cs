using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class FailedUnknownRepository
{
    public static async Task<bool> AddFailedUnknown(DataContext context, FailedUnknownEntity failedUnknown)
    {
        try
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                if (context.FailedUnknownEntity is null) throw new NullReferenceException("Datacontext FailedUnknownEntity is null.");
                var exists = await context.FailedUnknownEntity.AnyAsync((f) => f.UserId == failedUnknown.UserId);
                if (exists) return false;
                await context.FailedUnknownEntity.AddAsync(failedUnknown);

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