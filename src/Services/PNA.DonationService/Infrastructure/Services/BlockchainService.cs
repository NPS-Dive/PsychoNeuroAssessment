using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PNA.Core.Interfaces;

namespace PNA.DonationService.Infrastructure.Services;

public class BlockchainService : IBlockchainService
{
    private readonly IWeb3 _web3;
    private readonly IConfiguration _configuration;

    public BlockchainService ( IConfiguration configuration, IWeb3 web3 )
    {
        _configuration = configuration;
        _web3 = web3;
    }

    public async Task<string> ProcessDonationAsync ( decimal amount, Guid userId )
    {
        var recipientAddress = "0xSomeRecipientAddress"; // Replace with actual logic to determine recipient
        var tx = await _web3.Eth.GetEtherTransferService()
            .TransferEtherAsync(recipientAddress, amount);
        return tx;
    }
}