using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Strategy
{
    class HardStrategy : ISentenceStrategy
    {
        public int Difficulty
        {
            get
            {
                return 2;
            }
        }

        public float GetCost()
        {
            float number = -0.0025f;
            return number;
        }

        public Sentence GetNextSentence(List<Sentence> sentences)
        {
            sentences = sentences.OrderBy(s => s.Occurance).ToList();
            sentences[0].Occurance++;
            return sentences[0];
        }

        public float GetTranslationValue(int occurance)
        {
            float number = 0.35f / ((occurance + 1) / 1.5f);
            return number;
        }
    }
}
