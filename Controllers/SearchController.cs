using Microsoft.AspNetCore.Mvc;
using Search_Engine_Project.Core;
using Search_Engine_Project.Models;
using Search_Engine_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Search_Engine_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly WordService _WordService;
        private static Stopwatch watch;

        public SearchController(WordService WordService)
        {
            _WordService = WordService;
        }

        // GET: api/<SearchController>
        ///<summary>Endpoint to make a search query</summary>
        [HttpGet]
        public ActionResult<SearchResponse> Get(string query)
        {
            watch = new Stopwatch();
            watch.Start();
            List<String> splits = Semanter.Splitwords(query).ToList();
            HashSet<string> uniqueSplits = splits.ToHashSet();
            List<Word> words = _WordService.FindByKeywords(uniqueSplits);

            double queryTime = (double)watch.ElapsedMilliseconds;
            var results = Ranker.RankQueryDocuments(words, splits);
            double rankingTime = (double)watch.ElapsedMilliseconds - queryTime;

            watch.Stop();

            SearchResponse response = new SearchResponse(results, queryTime, rankingTime);
            return response; // response
        }
    }
}
