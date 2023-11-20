using Microsoft.Data.SqlClient;
namespace WordProcessorApp.Repositories
{
    public class Repository : IRepository
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True";
        public async Task CreateTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand();
                command.CommandText = """
                    IF object_id('Words') is null
                    CREATE TABLE  Words
                    (
                        Id INT IDENTITY PRIMARY KEY,
                        Word NVARCHAR(30) NOT NULL,
                        WordCount INT DEFAULT 0,
                    );   
                    """;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteRareWords()
        {
            string sqlExpression = "DELETE  FROM Words WHERE WordCount < 3;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteShortWords()
        {
            string sqlExpression = "DELETE  FROM Words WHERE LEN(Word) < 3;";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<string>> GetAll()
        {
            string sqlExpression = $$""""
                SELECT * FROM Words;
                """";
            var words = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(sqlExpression, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var word = reader.GetValue(1);
                            words.Add(word.ToString());
                        }
                    }
                    return words;
                }
            }
        }

        public async Task<List<string>> GetWordsWithBeginning(string start)
        {
            string sqlExpression = $""""
                SELECT TOP 5 * FROM Words
                    WHERE Word LIKE '{start}%'
                    ORDER BY WordCount DESC, Word ASC;
                """";
            var words = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(sqlExpression, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var word = reader.GetValue(1);
                            words.Add(word.ToString());
                        }
                    }
                    return words;
                }
            }
        }

        public async Task AddWord(string word)
        {
            var updateSqlExpression = $"""
                IF EXISTS(SELECT * FROM Words WHERE Word= '{word}')
                	UPDATE Words SET WordCount = WordCount + 1 WHERE Word = '{word}';
                ELSE
                	INSERT INTO WORDS(Word, WordCount) VALUES ('{word}', 1);
                """;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(updateSqlExpression, connection);

                var number = command.ExecuteNonQuery();
            }
        }

        public async Task DeleteData()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();
                command.CommandText = """
                    DELETE Words;
                    """;
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
