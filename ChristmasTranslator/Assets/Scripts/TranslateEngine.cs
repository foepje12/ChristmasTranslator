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

        private List<Sentence> sentences;
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
                controller.sacrificeButton.SetActive(false);
                hasAnswered = false;
                controller.UpdateSentence();
                shouldCountdown = true;
                SetRectangle(2);
                inputField.text = "";
            }

            //If we press enter in the inputField
            if (Input.GetKeyDown(KeyCode.Return) && inputField.text.Length > 0)
            {
                controller.sacrificeButton.SetActive(true);
                var answer = CheckAnswer(inputField.text);
                SetRectangle(answer ? 0 : 1);

                //The answer was correct!
                if (answer)
                {
                    var score = currentStrategy.GetTranslationValue(currentSentence.Occurance);

                    if (controller.hasRevealedAnswer)
                        score /= 2;

                    controller.totalScore += (int)Mathf.Round(1.4f * (currentStrategy.Difficulty + 1)) * 100;
                    controller.totalScoreText.text = controller.totalScore.ToString();
                    controller.ChangePoints(score);
                }
                else
                    controller.ChangePoints(-0.05f);

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

        /// <summary>
        /// Checks the given answer
        /// </summary>
        /// <param name="answer"></param>
        /// <returns>true if correct</returns>
        public bool CheckAnswer(string answer)
        {
            answer = answer.ToLower();

            var possibleAnswers = new List<string>
            {
                currentSentence.Answer.ToLower()
            };

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

        public void UpdateSentences()
        {
            sentences = GetSentencesFromFile(currentStrategy.Difficulty);
        }
    }
}
