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


        LoadPrompts(correctPrompts, "Assets/prompts/that-is-correct.txt");
        LoadPrompts(incorrectPrompts, "Assets/prompts/not-correct.txt");
        LoadPrompts(introPrompts, "Assets/prompts/which-is-fake.txt");

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
        SceneManager.LoadScene("Main");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

    private void LoadPrompts(List<string> list, string path)
    {

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
        }
        else
        {
            Debug.LogError("Text file not found: " + path);
        }
    }
}

[System.Serializable]
public struct Level
{
    public string name;
    public string source;
    public int answer;
}