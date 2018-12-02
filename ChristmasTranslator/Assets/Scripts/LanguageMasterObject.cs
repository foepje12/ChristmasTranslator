using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class RootObject
    {
        public List<Difficulty> Difficulties;
    }

    [Serializable]
    public class Difficulty
    {
        public string DifficultyValue;
        public List<Sentence> Sentences;
    }
    
    [Serializable]
    public class Sentence
    {
        public string Question;
        public string Answer;
        public string From;
        public string To;
        public List<string> Alternatives;
        public int Occurance;
    }

}
