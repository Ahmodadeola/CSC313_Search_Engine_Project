using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_Engine_Project.Core;

namespace Search_Engine_Project.Models{
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Value { get; set; }

        public Dictionary<string, WordFileDocument> Documents { get; set; }

        public Word()
        {
            
        }

        public Word(string value, Dictionary<string, WordFileDocument> documents, string id){
            Value = value;
            Id = id;
            Documents = documents;
        }
        
    }

}