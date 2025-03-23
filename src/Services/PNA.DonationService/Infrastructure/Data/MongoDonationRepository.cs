using MongoDB.Driver;
using PNA.Core.Entities;
using PNA.Core.Interfaces;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.DonationService.Infrastructure.Data
    {
    public class MongoDonationRepository : IDonationRepository
        {
        private readonly IMongoCollection<Donation> _donations;

        public MongoDonationRepository ( IConfiguration configuration )
            {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:Database"]);
            _donations = database.GetCollection<Donation>("Donations");
            }

        public async Task AddAsync ( Donation donation )
            {
            await _donations.InsertOneAsync(donation);
            }

        public async Task<Donation?> GetByIdAsync ( Guid id )
            {
            return await _donations.Find(d => d.Id == id).FirstOrDefaultAsync();
            }

        public async Task UpdateAsync ( Donation donation )
            {
            await _donations.ReplaceOneAsync(d => d.Id == donation.Id, donation);
            }
        }
    }