using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public int rows = 1;
    public int columns = 4;
    public bool autoCellSizing;
    public Vector2 spacing;
    public Vector2 cellSize;


    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();  

        if(autoCellSizing )
        {
            float sqRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqRt);
            columns = Mathf.CeilToInt(sqRt);
        }

        float parentWidth = rectTransform.rect.width;  
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2); // explain to me the math behind padding calcuation
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int rowCount = 0;
        int columnCount = 0;

        for ( int i = 0; i < rectChildren.Count; i++ )
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}
