namespace WordProcessorApp.Parsers;

public interface IParser
{
    Task<IEnumerable<string>> ParseFile(string path);
}
