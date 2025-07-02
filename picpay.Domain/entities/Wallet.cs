namespace PicPay.Domain.Entities;

public class Wallet
{
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }

    public User User { get; set; } = null!;
}
