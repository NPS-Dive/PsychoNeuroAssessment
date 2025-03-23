using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface ITestResultRepository : IBaseRepository<TestResult>
{
    Task<List<TestResult>> GetByUserIdAsync ( Guid userId );
}