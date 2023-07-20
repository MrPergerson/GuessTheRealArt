using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using static Unity.VisualScripting.Member;

public class GalleryViewer : MonoBehaviour
{
    public Transform galleryRoot;
    public GameObject artworkPrefab;

    private void Start()
    {
        var artworkCount = GameManager.instance.GetLevelCount();

        // generate all artworks
        for (int i = 0; i < artworkCount; i++)
        {
            var artwork = Instantiate(artworkPrefab, galleryRoot).GetComponent<Artwork>();
            //var index = i;

            var data = GameManager.instance.GetLevel(i+1);

            var fileName = data.source.Substring(12);

            var art = Resources.Load<Sprite>("level_assets/"+fileName+"/aaaaa_"+fileName); // will this work? no...
            Assert.IsNotNull(art);

            artwork.art.sprite = art;
            artwork.title.text = data.name;

        }
    }
}
