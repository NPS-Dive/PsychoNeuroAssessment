using PNA.Core.Enums;

namespace PNA.Core.Entities;

public class TestResult : BaseEntity
{
    public Guid UserId { get; set; }
    public TestType TestType { get; set; }
    public Dictionary<string, int> Responses { get; set; }
    public double Score { get; set; }
    public DateTime SubmittedAt { get; set; }

    public TestResult ( Guid userId, TestType testType, Dictionary<string, int> responses, double score, DateTime submittedAt )
    {
        UserId = userId;
        TestType = testType;
        Responses = responses;
        Score = score;
        SubmittedAt = submittedAt;
    }
}