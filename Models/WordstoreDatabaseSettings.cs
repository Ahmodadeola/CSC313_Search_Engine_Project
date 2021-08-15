namespace Search_Engine_Project.Models
{
    public class WordstoreDatabaseSettings : IWordstoreDatabaseSettings
    {
        public string WordsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IWordstoreDatabaseSettings
    {
        string WordsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}