using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/*  GameManager
 *  
 *  Responsiblies
 *  - Parse and cache category and leveldir json files
 *  - Store text prompts
 *  - Scene management 
 *  - Game state
 */

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Category[] _categories;

    public int level;
    private Level[] selectedLevels;

    public int answeredCorrectly = 0;

    public List<string> correctPrompts = new List<string>();
    public List<string> incorrectPrompts = new List<string>();
    public List<string> introPrompts = new List<string>();

    public Category[] Categories { 
        get { return _categories; } 
        private set { _categories = value; }
    }

    // load category list and prompts; initialize values
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }        
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        int seed = (int)System.DateTime.Now.Ticks;
        UnityEngine.Random.InitState(seed);

        string jsonPath = "categoryList";
        TextAsset jsonAsset = Resources.Load<TextAsset>(jsonPath);

        if (jsonAsset != null) // thanks chatgpt
        {
            string jsonContent = jsonAsset.text;
            Categories = ParseJsonArray<Category>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonPath);
        }


        LoadPrompts(correctPrompts, "prompts/that-is-correct");
        LoadPrompts(incorrectPrompts, "prompts/not-correct");
        LoadPrompts(introPrompts, "prompts/which-is-real");

    }

    // Caches the level directory json file for the specified category
    // This must be called before a level is loaded -- TODO: Add error handling
    public void SelectCategory(int category)
    {
        string jsonPath = Categories[category].source;
        TextAsset jsonAsset = Resources.Load<TextAsset>(jsonPath);

        if (jsonAsset != null) 
        {
            string jsonContent = jsonAsset.text;
            selectedLevels = ParseJsonArray<Level>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonPath);
        }

        level = 1;
        SceneManager.LoadScene("Main");


    }

    // Scene Management -----

    public void LoadNextScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var index = currentScene.buildIndex;

        SceneManager.LoadScene(index + 1);
    }

    public void StartGame()
    {
        level = 1;
        answeredCorrectly = 0;
        SceneManager.LoadScene("CategorySelect");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadLevel(int level)
    {
        //print(level + " >= " + levels.Length);
        if (level > selectedLevels.Length)
        {
            // Then there are no more levels, the game is over
            SceneManager.LoadScene("Score");
        }
        else
        {
        this.level = level;
        SceneManager.LoadScene("Main");

        }


    }

    // -----------------------
    
    private void LoadPrompts(List<string> list, string path)
    {

        TextAsset textAsset = Resources.Load<TextAsset>(path);

        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');

            foreach (string line in lines)
            {
                list.Add(line);
            }
        }
        else
        {
            Debug.LogError("Text file not found: " + path);
        }
    }

    public int GetLevelCount()
    {
        return selectedLevels.Length;
    }

    public Level GetCurrentLevel()
    {
        return selectedLevels[level - 1];
    }

    private T[] ParseJsonArray<T>(string json)
    {
        string fixedJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(fixedJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }

}

[System.Serializable]
public struct Level
{
    public string name;
    public string source;
    public int answer;
}

[System.Serializable]
public struct Category
{
    public string name;
    public string source;
    public string thumbnail;
}