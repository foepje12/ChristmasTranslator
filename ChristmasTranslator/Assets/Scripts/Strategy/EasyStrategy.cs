using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Strategy
{
    public class EasyStrategy : ISentenceStrategy
    {
        public int Difficulty
        {
            get
            {
                return 0;
            }
        }



        public float GetCost()
        {
            float number = -0.0010f;
            return number;

        }

        public float GetTranslationValue(int occurance)
        {
            float number = 0.20f / ((occurance + 1) / 1.5f);
            return number;
        }

        public Sentence GetNextSentence(List<Sentence> sentences)
        {
            sentences = sentences.OrderBy(s => s.Occurance).ToList();
            sentences[0].Occurance++;
            return sentences[0];
        }
    }
}
