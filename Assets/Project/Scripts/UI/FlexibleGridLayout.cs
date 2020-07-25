using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    [SerializeField]
    private FitType fitType = FitType.Uniform;
    [SerializeField]
    private int rows = 0;
    [SerializeField]
    private int columns = 0;

    [SerializeField]
    private Vector2 cellSize = Vector2.zero;
    [SerializeField]
    private Vector2 spacing = Vector2.zero;

    [SerializeField]
    private bool fitX = false;
    [SerializeField]
    private bool fitY = false;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        LimitSpacingAndPaddingBelowZero();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        else if (fitType == FitType.Height || fitType == FitType.FixedRows)
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);

        LimitRowsAndColumns();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / columns) - (spacing.x / columns * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / rows) - (spacing.x / rows * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int rowCount;
        int columnCount;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        new System.NotImplementedException();
    }

    #region Spacing and Padding Controller
    private void LimitSpacingAndPaddingBelowZero()
    {
        LimitSpacingBelowZero();
        LimitPaddingBelowZero();
    }

    private void LimitSpacingBelowZero()
    {
        if (spacing.x < 0)
            spacing.x = 0;
        if (spacing.y < 0)
            spacing.y = 0;
    }

    private void LimitPaddingBelowZero()
    {
        int top = padding.top;
        if (top < 0)
            top = 0;

        int bottom = padding.bottom;
        if (bottom < 0)
            bottom = 0;

        int left = padding.left;
        if (left < 0)
            left = 0;

        int right = padding.right;
        if (right < 0)
            right = 0;

        padding = new RectOffset(left, right, top, bottom);
    }
    #endregion

    private void LimitRowsAndColumns()
    {
        if (rows < 1)
            rows = 1;

        if (columns < 1)
            columns = 1;
    }
}
