using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Search_Engine_Project.Core
{

    public class Semanter
    {

        public HashSet<string> StopWords { get => _stopWords; }
        private HashSet<String> _stopWords = new HashSet<string>();

        public string FileName { get => _fileName; }
        private string _fileName;

        public string ParsedDocument { get => _parsedDocument; }
        private string _parsedDocument;

        private string stopWordsFile = "stopwords.txt";

  
        public Semanter(String DocumentName)
        {
            _fileName = DocumentName;
            try
            {
                string rootPath = Parser.getRootPath();
                string _stopWordsPath = System.IO.Path.Combine(rootPath, "assets", stopWordsFile);

                foreach (String word in File.ReadAllLines(_stopWordsPath))
                    _stopWords.Add(word);
            }
            catch (Exception ex)
            {
                throw new IOException("The specified path for stopwords could not be Read", ex);
            }

            // parse document
            string readText = Parser.ReadText(DocumentName);
            _parsedDocument = readText;
            
        }


        public Dictionary<string, int> getScannedDocumentData()
        {
            Dictionary<string, int> wordFrequencyMap = new Dictionary<string, int>(); 
            wordFrequencyMap.Add("docLength", 0);

            foreach (string splitWord in Semanter.Splitwords(ParsedDocument)) {
                wordFrequencyMap["docLength"]++;

                string word = splitWord.ToLower().Trim();
                if (_stopWords.Contains(word))
                    continue;
                
                if (wordFrequencyMap.ContainsKey(word))
                {
                    wordFrequencyMap[word]++;
                } else
                {
                    wordFrequencyMap.Add(word, 1);
                }
            }

            Dictionary<string, int> scannedResults = wordFrequencyMap;
            return scannedResults;
        }


        public static void printDictionary(Dictionary<string, int> keyValues)
        {
            foreach(KeyValuePair<string, int> entry in keyValues)
                Console.WriteLine(" - " + entry.Key + " ----> " + entry.Value);
        }


        /// <summary>
        ///  An array of all punctutions, can be used for splitting text into constituent words
        /// </summary>
        public static string[] punctuations = { " ", ",", "@", "#", "$", "%", "^", "&", "*", "+", "=", "`", "~", "<", ">", "/", "\\", "|", ":", "(", ")", "?", "!", ";", "-", "–", "_", "[", "]", "\"", ".", "…", "\t", "\n", "\r" };

        /// <summary>
        ///  Edits the specified query/document by splitting with punctuations and whitespaces.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static string[] Splitwords(string query)
        {
            return Regex.Replace(query.Trim().ToLower(), "'", string.Empty).Split(punctuations, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        ///  Edit the specified query/document by splitting with punctuations and whitespaces,
        ///  with exceptions of some punctuations.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="except">The string to exempted which splitting the query..</param>
        /// <returns>The query split into an array</returns>
        public static string[] Splitwords(string query, string except)
        {
            List<string> puncs = punctuations.ToList();
            puncs.Remove(except);
            return Regex.Replace(query.Trim().ToLower(), "'", string.Empty).Split(puncs.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// Edit the specified query/document by splitting with punctuations and whitespaces,
        /// with exceptions of some punctuations.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="except">The string to exempted which splitting the query..</param>
        /// <returns>The query split into an array</returns>
        public static string[] Splitwords(string query, string[] except)
        {
            List<string> puncs = punctuations.ToList();
            foreach (string rem in except)
                puncs.Remove(rem);
            return Regex.Replace(query.Trim().ToLower(), "'", string.Empty).Split(puncs.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

    }
}