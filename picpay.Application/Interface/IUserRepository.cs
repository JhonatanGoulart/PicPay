using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
}
