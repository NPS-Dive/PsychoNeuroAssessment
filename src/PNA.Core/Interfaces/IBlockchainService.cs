namespace PNA.Core.Interfaces;

public interface IBlockchainService
    {
    Task<string> ProcessDonationAsync ( decimal amount, Guid userId );
    }