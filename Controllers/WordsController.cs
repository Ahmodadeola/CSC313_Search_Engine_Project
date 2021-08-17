using Search_Engine_Project.Models;
using Search_Engine_Project.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Search_Engine_Project.Core;
using System;
namespace WordsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly WordService _WordService;

        public WordsController(WordService WordService)
        {
            
            _WordService = WordService;
        }

        [HttpGet]
        public ActionResult<List<Word>> Get(){
            Parser.GetTextFromText();
            return _WordService.Get();
        }
           

    //     [HttpGet("{id:length(24)}", Name = "GetWord")]
    //     public ActionResult<Word> Get(string id)
    //     {
    //         var Word = _WordService.Get(id);

    //         if (Word == null)
    //         {
    //             return NotFound();
    //         }

    //         return Word;
    //     }

    //     [HttpPost]
    //     public ActionResult<Word> Create(Word Word)
    //     {
    //         _WordService.Create(Word);

    //         return CreatedAtRoute("GetWord", new { id = Word.Id.ToString() }, Word);
    //     }

    //     [HttpPut("{id:length(24)}")]
    //     public IActionResult Update(string id, Word WordIn)
    //     {
    //         var Word = _WordService.Get(id);

    //         if (Word == null)
    //         {
    //             return NotFound();
    //         }

    //         _WordService.Update(id, WordIn);

    //         return NoContent();
    //     }

    //     [HttpDelete("{id:length(24)}")]
    //     public IActionResult Delete(string id)
    //     {
    //         var Word = _WordService.Get(id);

    //         if (Word == null)
    //         {
    //             return NotFound();
    //         }

    //         _WordService.Remove(Word.Id);

    //         return NoContent();
    //     }
    }
}