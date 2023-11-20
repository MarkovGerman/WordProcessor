namespace WordProcessorApp.Parsers;

public interface IParser
{
    Task<List<string>> ParseFile(string path);
}
