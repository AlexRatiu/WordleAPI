using ServerWordle.DAL.DBO;
using ServerWordle.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.Serialization;
using ServerWordle.Models;

namespace ServerWordle.DAL.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly WordsDbContext _dbContext;

        public WordRepository(WordsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Word word)
        { 
            _dbContext.Word.Add(word);
            _dbContext.SaveChanges();
        }

        public async Task<Word> GetById(int id_word)
        {
            return await _dbContext.Word.FindAsync(id_word);
        }

        public async Task<List<Word>> GetAll()
        {
            var results = _dbContext.Word.ToList();
            return results;
        }
    }
}
