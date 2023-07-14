using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryGroup : MonoBehaviour
{
    private CategorySelection[] selections;

    // Called when CategorySelector is generating categories
    // Returns all or some of the CategorySelections in this group
    // Count: Specifies the amount of selections this group should return. Any category that is 
    //  not returned becomes disabled. 
    public CategorySelection[] GetSelections(int count)
    {
        CategorySelection[] selections = transform.GetComponentsInChildren<CategorySelection>();
        CategorySelection[] export = new CategorySelection[count];

        for (int i = 0; i < selections.Length; i++)
        {

            if (i < count)
                export[i] = selections[i];
            else
                selections[i].gameObject.SetActive(false);
        }

        return export;
    }
}
