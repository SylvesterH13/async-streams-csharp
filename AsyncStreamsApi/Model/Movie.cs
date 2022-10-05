namespace AsyncStreamsApi.Model
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public int RuntimeInMinutes { get; set; }
    }
}
