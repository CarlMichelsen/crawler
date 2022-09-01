using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class UnknownRepository
{
    public static async Task<bool> RemoveUnknown(ulong unknownId)
    {
        try
        {
            using (var context = new DataContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                if (context.UnknownEntity is null) throw new NullReferenceException("datacontext UnknownEntity is null");
                
                var unknown = await context.UnknownEntity.FirstOrDefaultAsync((unk) => unk.Id == unknownId);
                if (unknown is not null)
                {
                    context.UnknownEntity.Remove(unknown);
                }

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