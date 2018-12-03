using Assets.Scripts;
using Assets.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text languageToTranslateTo;
    public Text textToTranslate;
    public Text actualTranslation;
    public Text totalScoreText;
    public int totalScore;

    public GameObject[] menus = new GameObject[3];
    public InputField inputField;
    public ElfController elfController;
    public GameObject gameOverPanel;
    public GameObject sacrificeButton;

    public bool hasRevealedAnswer = false;

    private TranslateEngine engine;
    private Image pointsLeftImage;
    private Image possiblePointImage;

    private float interval = 0.10f;
    private float pointsLeft = 1f;

    void Start()
    {
        engine = new TranslateEngine(this)
        {
            inputField = inputField
        };
    }

    void Update()
    {
        if (pointsLeft <= 0)
        {
            GameOver();
        }

        engine.Update();

        if (engine.shouldCountdown)
        {
            var possibleScore = engine.currentStrategy.GetTranslationValue(engine.currentSentence.Occurance);

            if (hasRevealedAnswer)
                possibleScore /= 2;

            possiblePointImage.fillAmount = pointsLeft + possibleScore;

            interval -= Time.deltaTime;

            if (interval <= 0)
            {
                interval = 0.050f;
                ChangePoints(engine.currentStrategy.GetCost());
            }
        }
    }

    public void ChangePoints(float amount)
    {
        pointsLeft += amount;

        if (pointsLeft > 1)
            pointsLeft = 1f;

        pointsLeftImage.fillAmount = pointsLeft;
    }

    public void StartGame()
    {
        //Menu UI
        menus[0].SetActive(false);
        menus[1].SetActive(false);

        //Game UI
        menus[2].SetActive(true);

        pointsLeftImage = GameObject.Find("CurrentPointStatus").GetComponent<Image>();
        possiblePointImage = GameObject.Find("PossiblePointStatus").GetComponent<Image>();

        engine.Start();
    }

    public void StopGame()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        if (gameOverPanel.activeSelf == false)
        {
            engine.Stop();
            gameOverPanel.SetActive(true);
        }
    }

    public void UpdateSentence()
    {
        hasRevealedAnswer = false;
        actualTranslation.text = "";
        Sentence sentence = engine.GetNewSentence();

        if (sentence.Occurance == 1)
            actualTranslation.text = sentence.Answer;

        languageToTranslateTo.text = sentence.To + ":";
        textToTranslate.text = sentence.Question;
    }

    public void RevealAnswer()
    {
        hasRevealedAnswer = true;
        actualTranslation.text = engine.currentSentence.Answer;
    }

    public void SacrificeElves()
    {
        if (engine.currentStrategy.Difficulty == 2)
            return;

        ISentenceStrategy strategy = null;

        switch (engine.currentStrategy.Difficulty)
        {
            case 0:
                strategy = new MediumStrategy();
                break;
            case 1:
                strategy = new HardStrategy();
                break;
            default:
                strategy = engine.currentStrategy;
                break;
        }

        elfController.SacrificeNextLayer();
        pointsLeft = 1f;
        engine.currentStrategy = strategy;
        engine.UpdateSentences();
    }
}
