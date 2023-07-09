
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public Transform imageRoot;
    public TextMeshProUGUI text;

    private SelectableChoice[] choices;

    private int[] choiceOrder = { 0, 1, 2, 3 };

    private Level levelData;

    public int nextLevelLoadDelay = 2;
    bool levelCompleted = false;


    private void Awake()
    {
        choices = new SelectableChoice[imageRoot.childCount];
    }

    private void Start()
    {
        if (imageRoot == null) Debug.LogError("LevelManager - imageRoot is not set!");

        levelData = GameManager.instance.GetCurrentLevel();

        LoadImages(levelData.source);

       
        for (int i = 0; i < imageRoot.childCount; i++)
        {
            var choice = imageRoot.GetChild(i).GetComponent<SelectableChoice>();
            int index = choice.choiceIndex;
            choice.onClick.AddListener(() => SelectChoice(index));

            choices[i] = choice;


        }

        text.text = GetRandomPrompt(GameManager.instance.introPrompts);

        ShuffleChoices();


    }

    public string GetRandomPrompt(List<string> prompts)
    {
        return prompts[Random.Range(0, prompts.Count)];
    }

    public void LoadImages(string source)
    {
        

        var sprites = Resources.LoadAll<Sprite>(source);

        for (int i = 0; i < sprites.Length; i++)
        {
            imageRoot.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = sprites[i];
        }
    }

    public void SelectChoice(int index)
    {
        if(levelCompleted) return;

        int answer = levelData.answer;
        var choice = choices[index - 1];

        /*
        if (index == 1) // WORKAROUND: Idk why but we have to switch the ends. 0 points to 3 and 3 points to 0....I really don't know why
            choice = choices[choices.Length - 1];
        else if(index == choices.Length)
            choice = choices[0];         
        */

        //print("Selected " + index + " | Current index " + choice.transform.GetSiblingIndex() + " | Answer: " + answer);

        levelCompleted = true;

        if (index == answer)
        {
            
            ColorBlock colors = choice.colors;
            colors.disabledColor = Color.green;
            choice.colors = colors;
            text.text = GetRandomPrompt(GameManager.instance.correctPrompts);
            GameManager.instance.answeredCorrectly += 1;
        } 
        else
        {
            ColorBlock colors = choice.colors;
            colors.disabledColor = Color.red;
            choice.colors = colors;
            text.text = GetRandomPrompt(GameManager.instance.incorrectPrompts);
        }

        for (int i = 0; i < choices.Length; i++)
            choices[i].interactable = false;

        StartCoroutine(LoadNextLevelWithDelay());
    }

    IEnumerator LoadNextLevelWithDelay()
    {
        yield return new WaitForSeconds(nextLevelLoadDelay);

        GameManager.instance.LoadLevel(GameManager.instance.level + 1);
    }

    void Shuffle(int[] array)
    {
        int n = array.Length;
        while(n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = array[k];
            array[k] = array[n];
            array[n] = temp;
        }
    }
    
    void ShuffleChoices()
    {
        Shuffle(choiceOrder);
        //print(choiceOrder[0] + " " + choiceOrder[1] + " " + choiceOrder[2] + " " + choiceOrder[3]);

        for (int i = 0; i < choiceOrder.Length; i++)
        {
            choices[i].transform.SetSiblingIndex(choiceOrder[i]);  
        }
    }
}
