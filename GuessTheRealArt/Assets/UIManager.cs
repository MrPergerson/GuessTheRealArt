using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        GameManager.instance.LoadNextScene();
    }

    public void ReturnToMenu()
    {
        GameManager.instance.ReturnToMenu();
    }
    
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }

    public void GotoCredits()
    {
        GameManager.instance.GotoCredits();
    }

    public void SelectCategory(int category)
    {
        GameManager.instance.SelectCategory(category);
    }
}
