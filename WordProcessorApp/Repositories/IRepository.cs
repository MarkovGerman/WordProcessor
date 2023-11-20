namespace WordProcessorApp.Repositories
{
    public  interface IRepository
    {
        Task CreateTable();
        Task DeleteData();
        Task AddWord(string word);
        Task DeleteRareWords();
        Task DeleteShortWords();
        Task<List<string>> GetAll();
        Task<List<string>> GetWordsWithBeginning(string start);
        
    }
}
