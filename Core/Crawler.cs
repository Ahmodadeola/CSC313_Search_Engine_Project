using Search_Engine_Project.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Search_Engine_Project.Core
{
    /**
     * crawls the folder of nun-indexed files and indexes them on the database
     * before moving them to the indexed folder.
     **/
    public class Crawler
    {
        private readonly Indexer _indexer;
        private readonly WordService _WordService;
        public Crawler(WordService wordService)
        {
            _WordService = wordService;
            _indexer = new Indexer(wordService);
        }
        public void crawl()
        {
            Console.WriteLine("---------- Crawling started -----------");

            string rootPath = Parser.getRootPath();
            string unindexedPath = Path.Combine(rootPath, "docs", "unindexed");
            string indexedPath = Path.Combine(rootPath, "webroot", "indexed");
            
            // loop through all files in unindexed folder
            foreach (string file in Directory.GetFiles(Path.Combine(unindexedPath))){
                string fileName = Path.GetFileName(file);
                Console.WriteLine($"Indexing {fileName}---");
                bool res =  _indexer.indexFile(fileName);
                if (res)
                {
                    string fileToMove = Path.Combine(unindexedPath, fileName);
                    string destination = Path.Combine(indexedPath, fileName);
                    
                    if (File.Exists(destination))
                    {
                        File.Delete(destination);
                    }
                    File.Move(fileToMove, destination, true);
                    if (File.Exists(fileToMove))
                    {
                        File.Delete(fileToMove);
                    }

                }
            }

            Console.WriteLine("---------- Crawling completed -----------");
        }

    }

}
