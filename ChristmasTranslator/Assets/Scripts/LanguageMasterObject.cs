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
        public string Words;
        public string English;
        public string Language;
        public List<string> Alternatives;
       // public int Occurance;
    }

}
