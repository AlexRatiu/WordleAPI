using Microsoft.VisualBasic;

namespace ServerWordle.DAL.DBO
{
    public class Word
    {
        public int? id_word {  get; set; }
        public string word_otd { get; set; }
        public DateOnly date {  get; set; }

    }
}
