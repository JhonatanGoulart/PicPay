namespace PicPay.Application.DTOs;

public class TransferRequest
{
    public Guid Payer { get; set; }
    public Guid Payee { get; set; }
    public decimal Value { get; set; }
}
