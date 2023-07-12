using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelector : MonoBehaviour
{
    public Transform categoryRoot;
    public GameObject categoryPrefab;

    private void Awake()
    {
        if (categoryRoot == null) Debug.LogError("Missing CategoryRoot");
        if (categoryPrefab == null) Debug.LogError("Missing CategoryPrefab");
    }

    private void Start()
    {
        var categories = GameManager.instance.Categories;

        for (int i = 0; i < categories.Length; i++)
        {

            var index = i;
            
            var selection = Instantiate(categoryPrefab, categoryRoot);
            selection.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.SelectCategory(index));
            selection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = categories[index].name;


        }
    }
}
