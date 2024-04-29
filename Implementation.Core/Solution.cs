using Implementation.Core.Helper;

namespace Implementation.Core;

public static class Solution
{
    #region counter

    /// <summary>
    /// Counts the number of times the words "Yes" and "No" occur in the <paramref name="input"/>.
    /// </summary>
    /// <param name="input">Input containing the words</param>
    /// <returns>The number of times "Yes" and "No" respectively occured int the <paramref name="input"/></returns>
    /// <exception cref="InvalidDataException">Is Thrown if the <pararef name="input"/> contains invalid characters or other words.</exception>
    public static object CountAnswers(this string input) => input.Split(',').Aggregate(
        new { YesCount = 0, NoCount = 0 },
        (agg, current) =>
        {
            return current.ToLower() switch
            {
                "yes" => agg with { YesCount = agg.YesCount + 1 },
                "no" => agg with { NoCount = agg.NoCount + 1 },
                // string.Empty not allowed
                "" => agg,
                _ => throw new InvalidDataException("The Input contains invalid answers")
            };
        });

    /// <inheritdoc cref="CountAnswers" />
    public static Result CountAnswersSpan(this string input)
    {
        if (input.Length == 0)
            return new Result(0, 0);

        ReadOnlySpan<char> chars = input;
        Span<char> currentWord = stackalloc char[3];
        var result = new Result(0, 0);
        var wordIndex = 0;

        foreach (char c in chars)
        {
            if (c == ',' || wordIndex == 3)
            {
                if (currentWord[0] == 'Y')
                    result = result with { YesCount = result.YesCount + 1 };

                if (currentWord[0] == 'N')
                    result = result with { NoCount = result.NoCount + 1 };

                wordIndex = 0;
                continue;
            }

            currentWord[wordIndex++] = c;
        }

        if (currentWord[0] == 'Y')
            result = result with { YesCount = result.YesCount + 1 };

        if (currentWord[0] == 'N')
            result = result with { NoCount = result.NoCount + 1 };

        return result;
    }

    #endregion

    #region decode

    /// <summary>
    /// Decodes the <paramref name="input"/> by extracting the A[N] into N times the character A.
    /// The values of A[N] must be separated by a ";".
    /// </summary>
    /// <param name="input">The encoded input</param>
    /// <returns>The decoded <paramref name="input"/></returns>
    /// <exception cref="InvalidDataException">Is thrown if the input contains invalid data or is corrupted</exception>
    static string Decode(this string input) => input.Split(";").Select(c => c switch
    {
        [var key] => key.ToString(),

        [var key, .. var count] => new string(key, Math.Max(int.Parse(count), 0)),

        _ => throw new InvalidDataException("Unexpected input")
    }).JoinChunks();

    #endregion
}