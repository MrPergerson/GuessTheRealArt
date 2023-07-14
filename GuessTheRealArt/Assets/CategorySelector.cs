using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelector : MonoBehaviour
{
    public RectTransform categoryRoot;
    public GameObject categoryPrefab;
    public Transform arrowRoot;

    private Button leftArrow;
    private Button rightArrow;

    public float sliderPosition = 0;
    [SerializeField]private float selectionWidth;
    [SerializeField] private float minPosition;

    private void Awake()
    {
        if (categoryRoot == null) Debug.LogError("Missing CategoryRoot");
        if (categoryPrefab == null) Debug.LogError("Missing CategoryPrefab");
        if (arrowRoot == null) Debug.LogError("Missing ArrowRoot");
    }

    private void Start()
    {
        leftArrow = arrowRoot.GetChild(0).GetComponent<Button>();
        leftArrow.onClick.AddListener(NavigateLeft);
        rightArrow = arrowRoot.GetChild(1).GetComponent<Button>();
        rightArrow.onClick.AddListener(NavigateRight); 

        var categories = GameManager.instance.Categories;

        for (int i = 0; i < categories.Length; i++)
        {

            var index = i;
            
            var selection = Instantiate(categoryPrefab, categoryRoot.transform);
            var categoryUI = selection.GetComponent<CategorySelection>();
            categoryUI.button.onClick.AddListener(() => GameManager.instance.SelectCategory(index));
            categoryUI.title.text = categories[index].name;


        }

        selectionWidth = categoryPrefab.GetComponent<RectTransform>().rect.width;
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
