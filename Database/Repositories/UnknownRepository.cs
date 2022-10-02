using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class UnknownRepository
{
    public static async Task<bool> RemoveUnknown(DataContext context, ulong unknownId)
    {
        try
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                if (context.UnknownEntity is null) throw new NullReferenceException("datacontext UnknownEntity is null");
                
                var unknown = await context.UnknownEntity
                    .FirstOrDefaultAsync((unk) => unk.Id == unknownId);

                if (unknown is not null) context.UnknownEntity.Remove(unknown);

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

    public static async Task<UnknownEntity?> GetNextUnknown(DataContext context)
    {
        if (context.UnknownEntity is null) throw new InvalidOperationException("Invalid UnknownEntity DataContext.");
        var next = await context.UnknownEntity
            .Where(unk => unk.Id != 0)
            .Include(unk => unk.User)
            .OrderByDescending(unk => unk.Recorded)
            .FirstOrDefaultAsync();
        if (next is null || next.User is null || next?.User.Id == 0) return null;
        return next;
    }

    public static async Task<UnknownEntity?> GetUnknownByUserId(DataContext context, ulong userId)
    {
        if (context.UnknownEntity is null) throw new InvalidOperationException("Invalid UnknownEntity DataContext.");
        return await context.UnknownEntity
            .Include(unk => unk.User)
            .Where(unk => unk.User.Id == userId)
            .FirstOrDefaultAsync();
    }
}