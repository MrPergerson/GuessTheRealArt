using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallerySelector : MonoBehaviour
{
    public Transform galleryRoot;
    public GameObject gallerySelectionPrefab;

    private void Awake()
    {
        if (galleryRoot == null) Debug.LogError("Missing GalleryRoot");
        if (gallerySelectionPrefab == null) Debug.LogError("Missing GallerySelectionPrefab");
    }

    private void Start()
    {
        var categories = GameManager.instance.Categories;

        for(int i = 0; i < categories.Length; i++)
        {
            var index = i;
            var galleryGameObject = Instantiate(gallerySelectionPrefab, galleryRoot.transform);
            var gallerySelection = galleryGameObject.GetComponent<GallerySelection>();

            gallerySelection.title.text = categories[i].name;
            
            // needs to go to gallery
            gallerySelection.button.onClick.AddListener(() => GameManager.instance.SelectCategory(index, true));

        }
    }
}
