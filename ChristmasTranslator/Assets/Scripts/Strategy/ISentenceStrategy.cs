using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface ISentenceStrategy
    {
        int Difficulty { get; }

        Sentence GetNextSentence(List<Sentence> sentences);
    }
}
