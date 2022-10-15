using AsyncStreamsApi.Model;
using System.Text;

namespace AsyncStreamsApi.Repositories
{
    public class MovieCsvRepository : IMovieRepository
    {
        private const string CSV_PATH = @"data\ratings.csv";

        private const int ID_INDEX = 0;
        private const int TITLE_INDEX = 3;
        private const int RATING_INDEX = 6;
        private const int RUNTIME_IN_MINUTES_INDEX = 7;
        private const int YEAR_INDEX = 8;

        private const int DEFAULT_BUFFER_SIZE = 4096;

        private readonly int _delayInMilliseconds;

        public MovieCsvRepository(IConfiguration configuration)
        {
            _delayInMilliseconds = configuration.GetValue<int>("StreamDelayInMilliseconds");
        }

        private static StreamReader AsyncStreamReader(string path, Encoding encoding)
            => new StreamReader(
                new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DEFAULT_BUFFER_SIZE, FileOptions.Asynchronous | FileOptions.SequentialScan),
                encoding, detectEncodingFromByteOrderMarks: true
            );

        public async IAsyncEnumerable<Movie> GetAsync()
        {
            using var streamReader = AsyncStreamReader(CSV_PATH, Encoding.UTF8);
            var header = await streamReader.ReadLineAsync();
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();

                // To be able to see the effect of IAsyncEnumerable
                await Task.Delay(_delayInMilliseconds);

                var values = line.Split(',');
                yield return new Movie
                {
                    Id = values[ID_INDEX],
                    Title = values[TITLE_INDEX],
                    Rating = double.Parse(values[RATING_INDEX]),
                    RuntimeInMinutes = int.Parse(values[RUNTIME_IN_MINUTES_INDEX]),
                    Year = int.Parse(values[YEAR_INDEX]),
                };
            }
        }
    }
}
