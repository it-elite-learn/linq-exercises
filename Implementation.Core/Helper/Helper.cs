namespace Implementation.Core.Helper;

public record Result(int YesCount, int NoCount);

public interface IFrom<TSource, TResult>
    where TResult : new()
{
    public static TResult From(TSource source) => new TResult();
}

public static class Helper
{
    public static string JoinChunks(this IEnumerable<string> chunks) => string.Join(string.Empty, chunks);

    public static Result Into(this object data) => new Result((data as dynamic).YesCount, (data as dynamic).NoCount);
}