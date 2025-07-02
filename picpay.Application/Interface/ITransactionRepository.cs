using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
}
