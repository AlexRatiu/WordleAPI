using ServerWordle.DAL.DBO;
using ServerWordle.DAL.Repositories.Interfaces;
using ServerWordle.Models;
using ServerWordle.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ServerWordle.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }


        // Hangfire logic to generate a new word everyday (saves only to the DB)
        public void GenerateWordOfTheCurrentDay()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, WordList.words.Count);

            var newWord = new Word
            {
                word_otd = WordList.words[randomIndex],
                date = DateOnly.FromDateTime(DateTime.Now)
            };

            _wordRepository.Create(newWord);
        }

        // GET endpoint
        public async Task<string> GetWordOfTheDay(DateOnly? date)
        {
            if (date == null)
            {
                var wordOfTheCurrentDay = _wordRepository.GetAll().Result.Where(x => x.date == DateOnly.FromDateTime(DateTime.Now)).Select(x => x.word_otd).FirstOrDefault();

                return wordOfTheCurrentDay;
            }
            else
            {
                var wordOfTheDay = _wordRepository.GetAll().Result.Where(x => x.date == date).Select(x => x.word_otd).FirstOrDefault();

                return wordOfTheDay;
            }
        }

        public async Task<List<string>> GetAllDates()
        {
            var dates = _wordRepository.GetAll().Result.Select(x => x.date.ToString()).ToList();
            return dates;
        }
    }
}
