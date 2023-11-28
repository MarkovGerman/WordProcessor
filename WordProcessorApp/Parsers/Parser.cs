using System.Text;
using System.Text.RegularExpressions;

namespace WordProcessorApp.Parsers;

public class Parser : IParser
{
    public async Task<IEnumerable<string>> ParseFile(string path)
    {
        using (var fstream = File.OpenRead(path))
        {
            // выделяем массив для считывания данных из файла
            var buffer = new byte[fstream.Length];
            // считываем данные
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            // декодируем байты в строку
            var s = Encoding.UTF8.GetString(buffer);
            return Parse(s);
        }
    }

    private IEnumerable<string> Parse(string text)
    {
        var regex = new Regex(@"[\wА-Яа-я\d]+[^\wА-Яа-я\d]");
        var matches = regex.Matches(text + " ");
        var words = new List<string>();
        foreach (Match match in matches)
        {
            var word = match.Value;
            word = String.Concat(word.Where(char.IsLetterOrDigit));
            yield return word;
        }
    }
}
