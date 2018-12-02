using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class TranslateEngine
    {
        public Sentence GetNewSentence(ISentenceStrategy strategy)
        {
            List<Sentence> sentences = GetSentencesFromFile(strategy.Difficulty);
            strategy.GetNextSentence(sentences);

            System.Random rand = new System.Random();
            var randInt = rand.Next(0, sentences.Count);
            return sentences[randInt];
        }

        public List<Sentence> GetSentencesFromFile(int difficulty)
        {
            RootObject x = JsonHandler.GetLanguageObject();
            return x.Difficulties[difficulty].Sentences.ToList();
        }
    }
}
