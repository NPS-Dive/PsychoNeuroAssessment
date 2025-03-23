using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PNA.Core.Interfaces;

namespace PNA.DonationService.Infrastructure.Services;

public class BlockchainService : IBlockchainService
{
    private readonly Web3 _web3;

    public BlockchainService ( IConfiguration configuration )
    {
        // Create the account
        var privateKey = configuration["Ethereum:PrivateKey"];
        var account = new Nethereum.Web3.Accounts.Account(privateKey);

        // Pass the account to Web3 constructor
        _web3 = new Web3(account, configuration["Ethereum:RpcUrl"]);
        _web3.TransactionManager.UseLegacyAsDefault = true; // For compatibility
    }

    public async Task<string> ProcessDonationAsync ( decimal amount, Guid userId )
    {
        var tx = await _web3.Eth.GetEtherTransferService()
            .TransferEtherAsync("RECEIVER_ADDRESS", amount); // Replace with actual receiver address
        return tx;
    }
}