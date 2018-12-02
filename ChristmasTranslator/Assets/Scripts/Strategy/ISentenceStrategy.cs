using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface ISentenceStrategy
    {
        int Difficulty { get; }

        float GetTranslationValue(int occurance);
        float GetCost();
        Sentence GetNextSentence(List<Sentence> sentences);
    }
}
