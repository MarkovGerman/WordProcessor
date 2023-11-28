namespace WordProcessorApp.Services;

public interface IDictionaryService
{
    Task CreateDictionary(string path);
    Task UpdateDictionary(string path);
    Task CleanDictionary();
    Task<List<string>> GetWordsWithBeginning(string start);
    Task CreateTable();
}
