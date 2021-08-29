using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_Engine_Project.Core;


namespace Search_Engine_Project.Core
{


	public class KeywordsDocument
	{

		public string DocumentName { get; private set; }
		public double DocumentLength { get; private set; }
		public double DocumentRank { get; set; }
		public string DocumentLink { get; set; }

		public Dictionary<string, double> KeywordsCount { get; private set; }

		public KeywordsDocument(string documentName, double documentLength)
		{
			DocumentName = documentName;
			DocumentLength = documentLength;
			KeywordsCount = new Dictionary<string, double>();
			DocumentRank = 0;
			DocumentLink = "";
		}

		public void AddKeywordFrequency(string keyword, double frequency)
		{
			KeywordsCount.Add(keyword, frequency);
		}

	}
}
