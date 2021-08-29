using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Search_Engine_Project.Core;


namespace Search_Engine_Project.Core
{


	public class SearchResponse
	{
		public List<KeywordsDocument> Documents { get; private set; }
		public double QueryTime { get; private set; }
		public double RankingTime { get; private set; }


		public SearchResponse(List<KeywordsDocument> documents, double queryTime, double rankingTime)
		{
			Documents = documents;
			QueryTime = queryTime;
			RankingTime = rankingTime;
		}

	}
}
