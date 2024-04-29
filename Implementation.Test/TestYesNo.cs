namespace Implementation.Test;

public class YesNo
{
    public static IEnumerable<TestCaseData> SimpleExampleSource
    {
        get
        {
            yield return new TestCaseData(string.Empty, new Result(0, 0));
            yield return new TestCaseData("Yes,No", new Result(1, 1));
            yield return new TestCaseData("Yes,No,Yes", new Result(2, 1));
            yield return new TestCaseData("Yes,Yes", new Result(2, 0));
            yield return new TestCaseData("No,No", new Result(0, 2));
            yield return new TestCaseData("Yes,Yes,Yes,No,Yes,No,No,Yes,Yes,No,No,No", new Result(6, 6));
        }
    }

    [TestCaseSource(nameof(SimpleExampleSource))]
    public void SimpleExample(string input, Result expected)
    {
        Result r = input.CountAnswers().Into();

        Assert.That(r, Is.EqualTo(expected));
    }

    [Test]
    public void ThrowOnInvalidData()
    {
        var input = "Yes,Nonono";

        Assert.Throws<InvalidDataException>(() => input.CountAnswers());
    }

    [TestCaseSource((nameof(SimpleExampleSource)))]
    public void SimpleExampleSpan(string input, Result expected)
    {
        var result = input.CountAnswersSpan();

        Assert.That(result, Is.EqualTo(expected));
    }
}