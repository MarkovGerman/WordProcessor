namespace WordProcessorApp.Repositories
{
    public interface IRepository
    {
        Task CreateTable();
        Task DeleteData();
        Task<List<string>> GetWordsWithBeginning(string start);
        Task AddWords(IEnumerable<KeyValuePair<string, int>> values);
    }
}
