using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Search_Engine_Project.Core;



namespace Search_Engine_Project.Core
{
	public class WordFileDocument
	{

		public string DocumentName { get; private set; }
		public int DocumentLength { get; private set; }

		public string Keyword { get; private set; }
		public int Frequency { get; private set; }

		public WordFileDocument(string documentName, int documentLength, string keywoard, int frequency)	
		{
			DocumentName = documentName;
			DocumentLength = documentLength;
			Keyword = keywoard;
			Frequency = frequency;
		}
	}
}

