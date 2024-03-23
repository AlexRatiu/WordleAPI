using ServerWordle.DAL.DBO;
using Microsoft.EntityFrameworkCore;

namespace ServerWordle.DAL.Repositories.Interfaces
{
    public interface IWordRepository
    {
        void Create(Word word);
        Task<Word> GetById(int id_word);

        Task<List<Word>> GetAll();
    }
}