using Microsoft.EntityFrameworkCore;
using PicPay.Application.Interfaces;
using PicPay.Domain.Entities;
using PicPay.Infrastructure.Data;

namespace PicPay.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Wallet)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
