namespace PicPay.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid PayerId { get; set; }
    public Guid PayeeId { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
