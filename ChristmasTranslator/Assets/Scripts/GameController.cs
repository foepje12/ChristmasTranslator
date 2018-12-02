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

    private TranslateEngine engine;

    // Use this for initialization
    void Start()
    {
        engine = new TranslateEngine();
        // currentStrategy = new EasyStrategy()        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextSentence()
    {
        //First start with easy
        Sentence sentence = engine.GetNewSentence(new EasyStrategy());
        languageToTranslateTo.text = sentence.Language + ":";
        textToTranslate.text = sentence.Words;
    }
}
