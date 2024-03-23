using ServerWordle.Models;
using ServerWordle.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ServerWordle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordsController
    {
        private readonly IWordService _wordService;

        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet("[action]")]
        public async Task<string> GetWordOfTheDay(DateTime? date)
        {
            if (date != null)
                return await _wordService.GetWordOfTheDay(DateOnly.FromDateTime((DateTime)date));

            return await _wordService.GetWordOfTheDay(null);
        }

        [HttpGet("[action]")]
        public async Task<List<string>> GetAllDates()
        {
            return await _wordService.GetAllDates();
        }
    }
}
