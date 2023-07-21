using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GalleryViewer : MonoBehaviour
{
    public Transform galleryRoot;
    public Transform arrowRoot;
    public GameObject artworkPrefab;
    public ScrollRect scrollView;
    public Button ExitGallery1;
    public Button ExitGallery2;

    private ArrowButton leftArrow;
    private ArrowButton rightArrow;
    //public int scrollRate = 1;

    private void Start()
    {

        leftArrow = arrowRoot.GetChild(0).GetComponent<ArrowButton>();
        rightArrow = arrowRoot.GetChild(1).GetComponent<ArrowButton>();

        ExitGallery1.onClick.AddListener(ReturnToGallerySelector);
        ExitGallery2.onClick.AddListener(ReturnToGallerySelector);  


        var artworkCount = GameManager.instance.GetLevelCount();

        // generate all artworks
        for (int i = 0; i < artworkCount; i++)
        {
            var artwork = Instantiate(artworkPrefab, galleryRoot).GetComponent<Artwork>();
            //var index = i;

            var data = GameManager.instance.GetLevel(i+1);

            var path = data.source + "/" + data.answer_filename;
            path = path.Substring(0, path.Length-4); // remove the .jpg

            //print(path);
            var art = Resources.Load<Sprite>(path);
            Assert.IsNotNull(art);

            artwork.art.sprite = art;
            artwork.title.text = data.name;

        }
    }

    private void Update()
    {
        if(leftArrow.isPressed)
            NavigateLeft();
        else if(rightArrow.isPressed) 
            NavigateRight();
    }

    private void NavigateRight()
    {
        scrollView.horizontalNormalizedPosition = (scrollView.horizontalNormalizedPosition + .003f);
    }

    private void NavigateLeft()
    {
        scrollView.horizontalNormalizedPosition = (scrollView.horizontalNormalizedPosition - .003f);


    }

    private void ReturnToGallerySelector()
    {
        SceneManager.LoadScene("GallerySelect");
    }
}
