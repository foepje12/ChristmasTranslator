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
               return 1;
            }
        }

        public Sentence GetNextSentence(List<Sentence> sentences)
        {
           // sentences.OrderBy(s => s.Occurance);
           // sentences[0].Occurance++;
           // Debug.Log(sentences[0].Occurance);
            return sentences[0];
        }
    }
}
