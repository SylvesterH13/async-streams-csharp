using AsyncStreamsApi.Model;
using Dapper;
using System.Data.SqlClient;

namespace AsyncStreamsApi.Repositories
{
    public class MovieSqlRepository : IMovieRepository
    {
        private const int DELAY_IN_MILLISECONDS = 50;

        private readonly string _connectionString;

        public MovieSqlRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalDB");
        }

        public async IAsyncEnumerable<Movie> GetAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var reader = await connection.ExecuteReaderAsync("SELECT * FROM Movies");

            var rowParser = reader.GetRowParser<Movie>();

            while (await reader.ReadAsync())
            {
                // To be able to see the effect of IAsyncEnumerable
                await Task.Delay(DELAY_IN_MILLISECONDS);

                yield return rowParser(reader);
            }
        }
    }
}
