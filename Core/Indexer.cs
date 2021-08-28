using Search_Engine_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Search_Engine_Project.Core
{
    public class Indexer
    {

        public bool indexFile(string file)
        {
            string[] words = { };
            Semanter semanter = new Semanter(file);
            Dictionary<string, int> scannedResults = semanter.getScannedDocumentData();
            foreach(K word in words)
            {
               
            }
            return true;
        }
    }
}