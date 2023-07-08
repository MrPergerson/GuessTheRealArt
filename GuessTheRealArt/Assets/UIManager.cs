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
}
