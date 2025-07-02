using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Entities;
using PicPay.Domain.Enums;

namespace PicPay.Application.Services;

public class TransferService
{
    private readonly IUserRepository _userRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransferService(
        IUserRepository userRepository,
        IWalletRepository walletRepository,
        ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<string> ExecuteAsync(TransferRequest request)
    {
        
        var payer = await _userRepository.GetByIdAsync(request.Payer);
        var payee = await _userRepository.GetByIdAsync(request.Payee);

        if (payer is null || payee is null)
            return "Usuário pagador ou recebedor não encontrado.";

        if (payer.Type == UserType.Merchant)
            return "Lojistas não podem enviar dinheiro.";

        var payerWallet = await _walletRepository.GetByUserIdAsync(payer.Id);
        var payeeWallet = await _walletRepository.GetByUserIdAsync(payee.Id);

        if (payerWallet is null || payeeWallet is null)
            return "Carteira não encontrada.";

        if (payerWallet.Balance < request.Value)
            return "Saldo insuficiente.";

       
        var authorized = await SimulateAuthorization();
        if (!authorized)
            return "Transação não autorizada pelo serviço externo.";

        
        payerWallet.Balance -= request.Value;
        payeeWallet.Balance += request.Value;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            PayerId = payer.Id,
            PayeeId = payee.Id,
            Value = request.Value
        };

        await _walletRepository.UpdateAsync(payerWallet);
        await _walletRepository.UpdateAsync(payeeWallet);
        await _transactionRepository.AddAsync(transaction);

       
        _ = Task.Run(() => SimulateNotification());

        return "Transferência realizada com sucesso.";
    }

    private async Task<bool> SimulateAuthorization()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://util.devi.tools/api/v2/authorize");
        return response.IsSuccessStatusCode;
    }

    private async Task SimulateNotification()
    {
        using var http = new HttpClient();
        var payload = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
        await http.PostAsync("https://util.devi.tools/api/v1/notify", payload);
    }
}
