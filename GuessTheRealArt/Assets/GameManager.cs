using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int level;
    private Level[] levels;

    public int answeredCorrectly = 0;

    public List<string> correctPrompts = new List<string>();
    public List<string> incorrectPrompts = new List<string>();
    public List<string> introPrompts = new List<string>();


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

        string jsonPath = "levelDirectory";
        TextAsset jsonAsset = Resources.Load<TextAsset>(jsonPath);

        if (jsonAsset != null) // thanks chatgpt
        {
            string jsonContent = jsonAsset.text;
            levels = ParseJsonArray<Level>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonPath);
        }


        LoadPrompts(correctPrompts, "prompts/that-is-correct");
        LoadPrompts(incorrectPrompts, "prompts/not-correct");
        LoadPrompts(introPrompts, "prompts/which-is-real");

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

    public Level GetCurrentLevel()
    {
        return levels[level - 1];
    }

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
        SceneManager.LoadScene("Main");
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
        if (level > levels.Length)
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
        return levels.Length;
    }

}

[System.Serializable]
public struct Level
{
    public string name;
    public string source;
    public int answer;
}