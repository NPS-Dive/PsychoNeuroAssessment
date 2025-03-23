using PNA.Core.Enums;

namespace PNA.Core.Entities;

public class TestQuestion : BaseEntity
{
    public TestType TestType { get; set; }
    public string QuestionText { get; set; }
    public List<string> Options { get; set; }

    public TestQuestion ( TestType testType, string questionText, List<string> options )
    {
        TestType = testType;
        QuestionText = questionText;
        Options = options;
    }
}