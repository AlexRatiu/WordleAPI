using ServerWordle.Models;

namespace ServerWordle.Services.Interfaces
{
    public interface IWordService
    {
        void GenerateWordOfTheCurrentDay();
        Task<string> GetWordOfTheDay(DateOnly? date);

        Task<List<string>> GetAllDates();
    }
}
