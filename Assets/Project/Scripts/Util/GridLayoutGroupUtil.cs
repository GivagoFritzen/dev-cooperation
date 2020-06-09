using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupUtil : MonoBehaviour
{
    public static void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        column = 1;
        row = 1;

        RectTransform firstChildObj = GetFirstChild(glg);

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        for (int i = 1; i < glg.transform.childCount; i++)
        {
            RectTransform currentChildObj = GetNextChild(glg, i);
            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            if (IsColumn(firstChildPos, currentChildPos))
            {
                column++;
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }
    }

    private static RectTransform GetFirstChild(GridLayoutGroup glg)
    {
        return glg.transform.GetChild(0).GetComponent<RectTransform>();
    }

    private static RectTransform GetNextChild(GridLayoutGroup glg, int index)
    {
        return glg.transform.GetChild(index).GetComponent<RectTransform>();
    }

    private static bool IsColumn(Vector2 firstChildPos, Vector2 currentChildPos)
    {
        return firstChildPos.x == currentChildPos.x;
    }
}
