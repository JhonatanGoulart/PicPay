using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByUserIdAsync(Guid userId);
    Task UpdateAsync(Wallet wallet);
}
