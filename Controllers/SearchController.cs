using Microsoft.AspNetCore.Mvc;
using Search_Engine_Project.Core;
using Search_Engine_Project.Models;
using Search_Engine_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Search_Engine_Project.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Search_Engine_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly WordService _WordService;

        public SearchController(WordService WordService)
        {
            _WordService = WordService;
        }
        // GET: api/<SearchController>
        [HttpGet]
        public ActionResult<List<KeywordsDocument>> Get(string query)
        {
            List<String> splits = Semanter.Splitwords(query).ToList();
            HashSet<string> uniqueSplits = splits.ToHashSet();
            List<Word> words = _WordService.FindByKeywords(uniqueSplits);

            return Ranker.RankQueryDocuments(words, splits);
        }
    }
}
