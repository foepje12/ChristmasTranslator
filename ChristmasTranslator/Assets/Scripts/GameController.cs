using Assets.Scripts;
using Assets.Scripts.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text languageToTranslateTo;
    public Text textToTranslate;

    public GameObject[] menus = new GameObject[3];
    public InputField inputField;

    private TranslateEngine engine;
    private Image pointsLeftImage;
    private Image possiblePointImage;

    private float interval = 0.10f;
    private float pointsLeft = 1f;

    void Start()
    {
        engine = new TranslateEngine(this);
        engine.inputField = inputField;        
    }

    void Update()
    {
        engine.Update();

        if (engine.shouldCountdown)
        {
            possiblePointImage.fillAmount = pointsLeft + engine.currentStrategy.GetTranslationValue(engine.currentSentence.Occurance);

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
        engine.Stop();

        //Menu UI
        menus[0].SetActive(true);
        menus[1].SetActive(true);

        //Game UI
        menus[2].SetActive(false);
    }

    public void UpdateSentence()
    {
        Sentence sentence = engine.GetNewSentence();
        languageToTranslateTo.text = sentence.To + ":";
        textToTranslate.text = sentence.Question;
    }
}
