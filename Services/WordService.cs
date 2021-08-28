using Search_Engine_Project.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Search_Engine_Project.Services
{
    public class WordService
    {
        private readonly IMongoCollection<Word> _Words;

        public WordService(IWordstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Console.WriteLine("db -- " + settings.WordsCollectionName);
            _Words = database.GetCollection<Word>(settings.WordsCollectionName);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<Word>.IndexKeys.Ascending(w => w.Value);
            var indexModel = new CreateIndexModel<Word>(indexKeys, indexOptions);
            _Words.Indexes.CreateOne(indexModel); 
        }

        public List<Word> Get() {
            Word[] words = { };
            return words.ToList();
        }

        // public Word Get(string id) =>
        //     _Words.Find<Word>(Word => Word.Id == id).FirstOrDefault();

        // public Word Create(Word Word)
        // {
        //     _Words.InsertOne(Word);
        //     return Word;
        // }

        // public void Update(string id, Word WordIn) =>
        //     _Words.ReplaceOne(Word => Word.Id == id, WordIn);

        // public void Remove(Word WordIn) =>
        //     _Words.DeleteOne(Word => Word.Id == WordIn.Id);

        // public void Remove(string id) => 
        //     _Words.DeleteOne(Word => Word.Id == id);
    }
}