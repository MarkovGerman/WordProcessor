using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using WordProcessorApp.Parsers;
using WordProcessorApp.Repositories;

namespace WordProcessorApp.Services;

public class DictionaryService : IDictionaryService
{
    private IRepository repository;
    private IParser parser;
    private ILogger<DictionaryService> log;
    private IMessageService messageService;

    public DictionaryService(IRepository repository, IParser parser, ILogger<DictionaryService> logger, IMessageService messageService)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "Logger\\log.txt");
        this.log = logger;
        this.repository = repository;
        this.parser = parser;
        this.messageService = messageService;
    }

    public async Task CreateDictionary(string path)
    {
        await CreateTable();
        await AddWordsDataBase(path);
        messageService.CreationMessage();
    }
    public async Task CreateTable()
    {
        try
        {
            await repository.CreateTable();
            log.LogInformation("Table create");
            await repository.DeleteData();
            log.LogInformation("Data in table delete!");
        }
        catch (Exception ex)
        {
            messageService.ShowError("Не удаётся создать таблицу!", $"Ошибка {ex.Message}");
        }
    }

    public async Task UpdateDictionary(string path)
    {
        await AddWordsDataBase(path);
        await SumWordCount();
        messageService.UpdatingMessage();
    }

    public async Task CleanDictionary()
    {
        try
        {
            await repository.DeleteData();
            log.LogInformation("Data in table delete!");
            messageService.CleaningMessage();
        }
        catch (Exception ex)
        {
            messageService.ShowError("Не удаётся очистить таблицу!", $"Ошибка {ex.Message}");
        }
    }
    public async Task<List<string>> GetWordsWithBeginning(string start)
    {
        var words = new List<string>();
        try
        {
            log.LogInformation($"Get words starting with {start}");
            words = await repository.GetWordsWithBeginning(start);
        }
        catch (Exception ex)
        {
            messageService.ShowError("Не удаётся очистить таблицу!", $"Ошибка {ex.Message}");
        }
        return words;
    }

    private async Task AddWordsDataBase(string path)
    {
        try
        {

            var dictionaryNumberWords = new Dictionary<string, int>();
            foreach (var word in await parser.ParseFile(path))
            {
                if (dictionaryNumberWords.ContainsKey(word))
                    dictionaryNumberWords[word]++;
                else dictionaryNumberWords[word] = 1;
            }

            var items = dictionaryNumberWords.Where(item => item.Key.Length >= 3 && item.Value >= 3).ToList();
            while (items.Any())
            {
                await repository.AddWords(items.Take(1000));
                items = items.Skip(1000).ToList();
            }
            log.LogInformation("Words add in db");
        }
        catch (Exception ex)
        {
            messageService.ShowError($"Не удаётся добавить слова в таблицу!", $"Ошибка {ex.Message}");
        }
    }
    private async Task SumWordCount()
    {
        try
        {
            await repository.SumNumberWords();
        }
        catch (Exception ex)
        {
            messageService.ShowError($"Не удаётся обновить количество слов в таблице!", $"Ошибка {ex.Message}");
        }
    }
}
