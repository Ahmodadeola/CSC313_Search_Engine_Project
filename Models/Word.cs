using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Search_Engine_Project.Models{
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Value { get; set; }

        public string Url { get; set; }

        public Word(string id, string value, string url){
            Value = value;
            Url = url;
            Id = id;
        }
        
    }

}