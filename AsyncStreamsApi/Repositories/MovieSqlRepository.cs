using AsyncStreamsApi.Model;
using Dapper;
using System.Data.SqlClient;

namespace AsyncStreamsApi.Repositories
{
    public class MovieSqlRepository : IMovieRepository
    {
        private readonly string _connectionString;
        private readonly int _delayInMilliseconds;

        public MovieSqlRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalDB");
            _delayInMilliseconds = configuration.GetValue<int>("StreamDelayInMilliseconds");
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
                await Task.Delay(_delayInMilliseconds);

                yield return rowParser(reader);
            }
        }
    }
}
