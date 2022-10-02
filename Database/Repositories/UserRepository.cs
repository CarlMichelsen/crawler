using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public static class UserRepository
{
    public static async Task<UserEntity?> GetUserById(DataContext context, ulong? userId)
    {
        if (userId is null) return null;
        if (context.UserEntity is null) 
            throw new InvalidOperationException("Invalid UnknownEntity DataContext.");
        
        return await context.UserEntity
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
    }
}