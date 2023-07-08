using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform imageRoot;

    private void Start()
    {
        if (imageRoot == null) Debug.LogError("LevelManager - imageRoot is not set!");

        Level level = GameManager.instance.GetCurrentLevel();

        LoadImages(level.source);
    }

    public void LoadImages(string source)
    {
        

        var sprites = Resources.LoadAll<Sprite>(source);

        for (int i = 0; i < sprites.Length; i++)
        {
            imageRoot.transform.GetChild(i).GetComponent<Image>().sprite = sprites[i];
        }
    }
}
