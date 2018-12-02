using Assets.Scripts.Strategy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TranslateEngine
    {
        public ISentenceStrategy currentStrategy;
        public InputField inputField;
        public bool shouldCountdown = false;
        public Sentence currentSentence;

        private readonly List<Sentence> sentences;
        private GameController controller;
        private bool hasStarted = false;
        private bool hasAnswered = false;


        public TranslateEngine(GameController contr)
        {
            controller = contr;
            currentStrategy = new EasyStrategy();
            sentences = GetSentencesFromFile(currentStrategy.Difficulty);
        }

        /// <summary>
        /// Starts the game and times
        /// </summary>
        public void Start()
        {
            hasStarted = true;
            controller.UpdateSentence();
            shouldCountdown = true;
        }

        /// <summary>
        /// Updates the timers
        /// </summary>
        public void Update()
        {
            //If the game has not started, return
            if (hasStarted == false)
                return;

            if (Input.GetKeyDown(KeyCode.Return) && hasAnswered)
            {
                hasAnswered = false;
                controller.UpdateSentence();
                shouldCountdown = true;
                SetRectangle(2);
            }

            //If we press enter in the inputField
            if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0)
            {
                var answer = CheckAnswer(inputField.text);
                SetRectangle(answer ? 0 : 1);

                if (answer)
                {
                    controller.ChangePoints(currentStrategy.GetTranslationValue(currentSentence.Occurance));
                }

                inputField.text = "";
                hasAnswered = true;
                shouldCountdown = false;
            }

            inputField.Select();
            inputField.ActivateInputField();
        }

        /// <summary>
        /// Stops the game
        /// </summary>
        public void Stop()
        {
            hasStarted = false;
        }

        private void SetRectangle(int mode)
        {
            var color = mode == 0 ? new Color(0.3884734f, 0.9433962f, 0.2892489f) : mode == 1 ? new Color(0.945098f, 0.3768993f, 0.2901961f) : new Color(0, 0, 0, 0);
            GameObject.Find("AnswerRectangle").GetComponent<Image>().color = color;
        }

        public bool CheckAnswer(string answer)
        {
            answer = answer.ToLower();

            var possibleAnswers = new List<string>();
            possibleAnswers.Add(currentSentence.Answer.ToLower());

            //If there are alternative sentences, add them to the list of possible answers
            if (currentSentence.Alternatives != null && currentSentence.Alternatives.Count > 0)
                foreach (string s in currentSentence.Alternatives)
                    possibleAnswers.Add(s.ToLower());

            return possibleAnswers.Contains(answer);
        }

        public Sentence GetNewSentence()
        {
            var sentence = currentStrategy.GetNextSentence(sentences);
            currentSentence = sentence;
            return sentence;
        }

        public List<Sentence> GetSentencesFromFile(int difficulty)
        {
            RootObject x = JsonHandler.GetLanguageObject();
            return x.Difficulties[difficulty].Sentences.ToList();
        }
    }
}
