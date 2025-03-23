using PNA.Core.Enums;

namespace PNA.Core.Entities;

public class Donation : BaseEntity
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public BlockchainType BlockchainType { get; set; }
    public string TransactionHash { get; set; }
    public DateTime DonatedAt { get; set; }

    public Donation ( Guid userId, decimal amount, BlockchainType blockchainType, string transactionHash, DateTime donatedAt )
    {
        UserId = userId;
        Amount = amount;
        BlockchainType = blockchainType;
        TransactionHash = transactionHash;
        DonatedAt = donatedAt;
    }
}