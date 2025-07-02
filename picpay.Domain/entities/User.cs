using PicPay.Domain.Enums;

namespace PicPay.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string CPF_CNPJ { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserType Type { get; set; }

    public Wallet Wallet { get; set; } = null!;
}
