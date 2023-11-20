namespace WordProcessorApp.Services;

public interface IDictionaryService
{
    Task CreateDictionary(string path);
    Task UpdateDictionary(string path);
    Task CleanDictionary();
    Task<List<string>> GetAll();
    Task<List<string>> GetWordsWithBeginning(string start);
    Task AddWord(string word);
    Task CreateTable();
}
