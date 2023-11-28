using Microsoft.Data.SqlClient;
using System.Security.Principal;
using System.Windows.Forms;

namespace WordProcessorApp.Repositories
{
    public class Repository : IRepository
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True";
        public async Task CreateTable()
        {
            var sqlExpression = $"""
                    IF object_id('Words') is null
                    CREATE TABLE  Words
                    (
                        Id INT IDENTITY PRIMARY KEY,
                        Word NVARCHAR(30) NOT NULL,
                        WordCount INT DEFAULT 0,
                    ); 
                    """;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<string>> GetWordsWithBeginning(string start)
        {
            var sqlExpression = $""""
                SELECT DISTINCT TOP 5 Word, WordCount FROM Words
                    WHERE Word LIKE N'{start}%'
                    ORDER BY WordCount DESC, Word ASC;
                """";

            var words = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(sqlExpression, connection);
                await command.Connection.OpenAsync();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var word = reader.GetValue(0);
                            words.Add(word.ToString());
                        }
                    }
                    return words;
                }
            }
        }
        public async Task SumNumberWords()
        {
            var sqlExpression = $"""
            IF object_id('WordsCopy') is null
                CREATE TABLE  WordsCopy
                (
                    Id INT IDENTITY PRIMARY KEY,
                    Word NVARCHAR(30) NOT NULL,
                    WordCount INT DEFAULT 0,
                );
            INSERT INTO WordsCopy SELECT Word, SUM(WordCount) AS WordCount FROM Words GROUP BY Word;
            DELETE Words;
            INSERT INTO Words SELECT Word, WordCount FROM WordsCopy;
            DELETE WordsCopy;
            """;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(sqlExpression, connection);
                var number = command.ExecuteNonQuery();
            }
        }
        public async Task AddWords(IEnumerable<KeyValuePair<string, int>> values)
        {
            var words = values.Select(item => $"(N'{item.Key}', {item.Value})");
            var sqlExpression = $"""
                INSERT INTO WORDS(Word, WordCount) VALUES {string.Join(", ", words)}
                """;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(sqlExpression, connection);
                var number = command.ExecuteNonQuery();
            }
        }
        public async Task DeleteData()
        {
            var sqlExpression = $"DELETE Words;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
