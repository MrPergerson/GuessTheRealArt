using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class CategorySelector : MonoBehaviour
{
    public RectTransform categoryRoot;
    //public GameObject categoryPrefab;
    public GameObject categoryGroupPrefab;
    public Transform arrowRoot;

    private Button leftArrow;
    private Button rightArrow;

    public float sliderPosition = 0;
    [SerializeField]private float selectionWidth;
    [SerializeField] private float minPosition;

    private void Awake()
    {
        if (categoryRoot == null) Debug.LogError("Missing CategoryRoot");
        if (categoryGroupPrefab == null) Debug.LogError("Missing CategoryPrefab");
        if (arrowRoot == null) Debug.LogError("Missing ArrowRoot");
    }

    private void Start()
    {
        leftArrow = arrowRoot.GetChild(0).GetComponent<Button>();
        leftArrow.onClick.AddListener(NavigateLeft);
        rightArrow = arrowRoot.GetChild(1).GetComponent<Button>();
        rightArrow.onClick.AddListener(NavigateRight); 

        var categories = GameManager.instance.Categories;
        int groupCount = Mathf.FloorToInt(categories.Length / 3);
        int leftOvers = categories.Length % 3;

        var categoryIndex = 0;
        for (int i = 0; i <= groupCount; i++)
        {
            var groupGameObject = Instantiate(categoryGroupPrefab, categoryRoot.transform);
            var selectionGroup = groupGameObject.GetComponent<CategoryGroup>();

            int selectionCount = 3;
            if (i == groupCount)
                selectionCount = leftOvers;

            var selections = selectionGroup.GetSelections(selectionCount);

            
            for (int j = 0; j < selections.Length; j++)
            {
                var c = categoryIndex;
                selections[categoryIndex].button.onClick.AddListener(
                    () => GameManager.instance.SelectCategory(c));
                selections[categoryIndex].title.text = categories[c].name;

                var thumbnail = Resources.Load<Sprite>(categories[c].thumbnail);
                if (thumbnail != null)
                    selections[categoryIndex].image.sprite = thumbnail;
                else
                {
                    Debug.LogError("Thumbnail.png not found in " + categories[c].thumbnail);
                }
                
                categoryIndex++;
            }

            


        }

        selectionWidth = categoryGroupPrefab.GetComponent<RectTransform>().rect.width;
        minPosition = -1 * selectionWidth * (categories.Length - 1);


    }

    public void NavigateLeft()
    {
        if (sliderPosition < 0)
        {
            sliderPosition += selectionWidth;
        }
    }

    public void NavigateRight()
    {
        if (sliderPosition > minPosition)
        {
            sliderPosition -= selectionWidth;
        }
    }

    private void Update()
    {
        categoryRoot.anchoredPosition = new Vector2(sliderPosition, 0);
    }
}
