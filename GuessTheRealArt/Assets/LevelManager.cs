using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public Transform imageRoot;

    private SelectableChoice[] choices;

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

        print("Selected " + index + " | Current index " + choice.transform.GetSiblingIndex() + " | Answer: " + answer);

        levelCompleted = true;

        if (index == answer)
        {
            
            ColorBlock colors = choice.colors;
            colors.disabledColor = Color.green;
            choice.colors = colors;        
        } 
        else
        {
            ColorBlock colors = choice.colors;
            colors.disabledColor = Color.red;
            choice.colors = colors;
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
}
