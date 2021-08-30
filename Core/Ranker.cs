using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Search_Engine_Project.Models;

namespace Search_Engine_Project.Core
{
    public class Ranker
    {
      /**
       ----Needed Input for query ranking----
        List of all Documents (Name + Length)
        List of all Query words (From word Object having each document and it's frequency in the document"
        word
            -- Value
            -- Documents
               -- document -> { DocumentName, DocumentLength, Frequency }
        
        ALGORITHM
        1. let each word in the query be a term
        2. Compute The Term Frequency TF(t, d) of each term t in each document d
           TF(t, d) = N(t, d), wherein (t, d) = term frequency for a term t in document d.
           N(t, d)  = number of times a term t occurs in document d

        3. Compute Inverse document frequency of each term t - IDF
           df(t) = Document Frequency of a term = N(t)
           N(t) = Number of documents containing the term t
           The idf of a term is the number of documents in the corpus divided by the document frequency of a term.

           idf(t) = LOG(N/ df(t)) = LOG(N/N(t))

        4. TF-IDF Scoring
            Now we have defined both tf and idf and now we can combine these
            to produce the ultimate score of a term t in document d. Therefore,
          
            tf-idf(t, d) = tf(t, d) * idf(t, d)
        
        5. Find Similarity Using Cosine Similarity Method
       */

        private static string _rootPath = Parser.getRootPath();

        public static List<KeywordsDocument> RankQueryDocuments(List<Word> WordList, List<string> query)
        {

            // Parse Word list into KeywordsDocuments format
            Dictionary<string, KeywordsDocument> ParsedDocuments = new Dictionary<string, KeywordsDocument>();
            Dictionary<string, double> DocumentFrequencies = new Dictionary<string, double>();

            foreach(Word wordObj in WordList)
            {
                string keyword = wordObj.Value;
                double documentsCount = wordObj.Documents.Count();

                foreach (KeyValuePair<string, WordFileDocument> entry in wordObj.Documents)
                {
                    string documentName = entry.Key;
                    WordFileDocument documentObj = entry.Value;
                    double keywordFrequencyInDocument = documentObj.Frequency;
                    double documentLength = documentObj.DocumentLength;
                    
                    if (!ParsedDocuments.ContainsKey(documentName))
                    {
                        KeywordsDocument doc = new KeywordsDocument(documentName, documentLength);
                        ParsedDocuments.Add(documentName, doc);

                    }
                       
                    ParsedDocuments[documentName].AddKeywordFrequency(keyword, keywordFrequencyInDocument);
                }

                if (!DocumentFrequencies.ContainsKey(keyword)){
                    DocumentFrequencies.Add(keyword, 1);
                }
                else {
                    DocumentFrequencies[keyword]++;
                }
            } 
            
            double totalDocumentCount = ParsedDocuments.Count() + 1;
            double[] queryVector = GetVectorFromQuery(query, totalDocumentCount);

            return ComputeWeightsAndRank(ParsedDocuments, DocumentFrequencies, totalDocumentCount, queryVector);
            
        }

        private static List<KeywordsDocument> ComputeWeightsAndRank(
          Dictionary<string, KeywordsDocument> ParsedDocuments,
          Dictionary<string, double> DocumentFrequencies, 
          double totalDocumentsCount,
          double[] queryVector)
        {
            // Calculate TF-IDF weights for each term/keyword for a document d
            var TFIDFWeights = new Dictionary<string, Dictionary<string, double>>();
            List<KeywordsDocument> sortedDocuments = new List<KeywordsDocument>();

            foreach(KeyValuePair<string, KeywordsDocument> documentEntry in ParsedDocuments) {

                string documentName = documentEntry.Key;
                KeywordsDocument keywordsDocument = documentEntry.Value;

                List<double> Vector = new List<double>();
                 
                foreach(KeyValuePair<string, double> keywordsFrequencyPair in keywordsDocument.KeywordsCount)
                {
                    string keyword = keywordsFrequencyPair.Key;
                    double frequency = keywordsFrequencyPair.Value;

                    // Normalize the TF for the keyword in the document
                    double TFWeight = (frequency / (keywordsDocument.DocumentLength + 1));

                    // Get DF for the keyword/term
                    double DFWeight = DocumentFrequencies[keyword];

                    // Get IDF for the keyword/term
                    double IDFWeight = GetIDFWeight(totalDocumentsCount, DFWeight);


                    // Get TF-IDF score for the document based on keyword
                    double TFIDFWeight = TFWeight * IDFWeight;

                    if (!TFIDFWeights.ContainsKey(documentName))
                        TFIDFWeights.Add(documentName, new Dictionary<string, double>());

                    TFIDFWeights[documentName].Add(keyword, TFIDFWeight);
                    Vector.Add(TFIDFWeight);
                }



                // Create Document Vector
                double[] documentVector = Vector.ToArray();

                string baseUrl = "https://localhost:5001/";
                // Compute Cosine Similarity For Document and Query and Get Rank Of Document.
                double rankScore = ComputeCosineAngularRank(documentVector, queryVector);
                keywordsDocument.DocumentRank = rankScore;
                keywordsDocument.DocumentLink =  baseUrl + "indexed/" + documentName;
                sortedDocuments.Add(keywordsDocument);
            }

            var rankedDocuments = sortedDocuments.OrderByDescending(x => x.DocumentRank).ToList();
            return rankedDocuments;
        }


        private static double ComputeCosineAngularRank(double[] VectorA, double[] VectorB)
        {
            int n = VectorA.Length;
            double normOfVectorA = 0;
            double normOfVectorB = 0;
            double dotProduct = 0;
            for (int i = 0; i < n; i++) {
                dotProduct += VectorA[i] * VectorB[i];
                normOfVectorA += Math.Pow(VectorA[i], 2);
                normOfVectorB += Math.Pow(VectorB[i], 2);
            }

            normOfVectorA = Math.Sqrt(normOfVectorA);
            normOfVectorB = Math.Sqrt(normOfVectorB);


            double result = dotProduct / (normOfVectorA * normOfVectorB);

            return result; 
        }



        private static double[] GetVectorFromQuery(List<string> query, double totalDocumentsCount) 
        {
            List<double> result = new List<double>();
            HashSet<string> querySet = new HashSet<string>(query);
            foreach (string word in querySet) {
                result.Add(query.FindAll(x => x == word).Count);
            }

            double[] queryVector = result.ToArray();
            for (int i = 0; i < queryVector.Length; i++) {
                queryVector[i] /= queryVector.Length; // Normalize frequency and set TF.
                queryVector[i] *= GetIDFWeight(totalDocumentsCount, queryVector.Length); // set the TF-IDF score.
            }

            return queryVector;
        }

        private static double TfWeight(double count) {
            return (count == 0) ? 0 : 1 + Math.Log(count);
        }

        private static double GetIDFWeight(double totalDocumentsCount, double DFweight) {
            return (totalDocumentsCount > 0) ? 1 + Math.Log(totalDocumentsCount/DFweight) : 0;
        }



        
        /*
        public static List<KeywordsDocument> TestRankQueryDocuments()
        {   
            WordFileDocument doc5 = new WordFileDocument("Test Document 5.txt", 200, "wiring", 6);
            WordFileDocument doc2 = new WordFileDocument("Test Document 2.txt", 200, "destination", 3);
            WordFileDocument doc3 = new WordFileDocument("Test Document 3.txt", 226, "wiring", 4);
            WordFileDocument doc4 = new WordFileDocument("Test Document 4.txt", 228, "blow", 6);
            WordFileDocument doc1 = new WordFileDocument("Test Document 1.txt", 289, "wiring", 4);

            var wiring = new Dictionary<string, WordFileDocument>();
            wiring.Add(doc1.DocumentName, doc1);
            wiring.Add(doc3.DocumentName, doc3);
            wiring.Add(doc5.DocumentName, doc5);

            var destination = new Dictionary<string, WordFileDocument>();
            destination.Add(doc2.DocumentName, doc2);

            var blow = new Dictionary<string, WordFileDocument>();
            blow.Add(doc4.DocumentName, doc4);

    
            Word word1 = new Word("wiring", wiring, "1");
            Word word2 = new Word("destination", destination, "2");
            Word word3 = new Word("blow", blow, "3");


            List<Word> list = new List<Word>();
            list.Add(word1);
            list.Add(word2);
            list.Add(word3);
         
            List<string> query = new List<string>();
            query.Add("wiring");
            query.Add("destination");
            query.Add("blow");

            var result = RankQueryDocuments(list, query);
            
            return result;
        } */






    }
}