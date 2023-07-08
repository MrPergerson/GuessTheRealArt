using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabImages : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sprites = Resources.LoadAll<Sprite>("test");

        for (int i = 0; i < sprites.Length; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = sprites[i];
        }
    }

}
