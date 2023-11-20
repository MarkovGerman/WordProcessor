using Serilog;
using Serilog.Core;
using System.Reflection;
using WordProcessorApp.Parsers;
using WordProcessorApp.Repositories;

namespace WordProcessorApp.Services;

public class DictionaryService : IDictionaryService
{
    private IRepository repository;
    private IParser parser;
    private Logger log;

    public DictionaryService(IRepository repository)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "Logger\\log.txt");

        log = new LoggerConfiguration()
            .WriteTo.File(filePath)
    .       CreateLogger();

        this.repository = repository;
        parser = new Parser();
    }

    public async Task CreateDictionary(string path)
    {
        await repository.CreateTable();
        log.Information("Table create");
        await repository.DeleteData();
        log.Information("Data in table delete!");
        foreach (var word in await parser.ParseFile(path))
        {
            await repository.AddWord(word);
            log.Information($"{word} is added");
        }
        await repository.DeleteRareWords();
        log.Information("Rare words removed");

        await repository.DeleteShortWords();
        log.Information("Short words removed");
    }
    public async Task CreateTable()
    {
        await repository.CreateTable();
        log.Information("Table create");
        await repository.DeleteData();
        log.Information("Data in table delete!");
    }

    public async Task UpdateDictionary(string path)
    {
        await parser.ParseFile(path);
        log.Information($"File {path} was parsed");
        await repository.DeleteRareWords();
        log.Information("Rare words removed");

        await repository.DeleteShortWords();
        log.Information("Short words removed");

    }

    public async Task AddWord(string word)
    {
        if (word.Length >= 3)
        {
            await repository.AddWord(word);
            log.Information($"{word} is added");
        }
    }

    public async Task CleanDictionary()
    {
        await repository.DeleteData();
        log.Information("Data in table delete!");
    }

    public async Task<List<string>> GetAll()
    {
        log.Information("Get All");
        return await repository.GetAll();
    }
    public async Task<List<string>> GetWordsWithBeginning(string start)
    {
        log.Information($"Get words starting with {start}");
        return await repository.GetWordsWithBeginning(start);
    }
}
