using Search_Engine_Project.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Search_Engine_Project.Services
{
    public class WordService
    {
        // private readonly IMongoCollection<Word> _Words;
        private readonly Word[] _Words;

        public WordService(IWordstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            // _Words = database.GetCollection<Word>(settings.WordsCollectionName);
            string[] words = new string[]{ "boy", "lover", "food", "tired"};
            var rng = new Random();
            _Words = Enumerable.Range(0, 3).Select(index => new Word(index.ToString(), words[index], "/documents.pdf"))
            .ToArray();

        }

        public List<Word> Get() =>
            _Words.ToList();

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