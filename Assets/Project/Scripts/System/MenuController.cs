using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Inputs Controller")]
    [SerializeField]
    private float delay = 0.5f;
    private float inputController = Mathf.Infinity;
    [SerializeField]
    private int currentMenuVertical = 1;
    [SerializeField]
    private int currentMenuHorizontal = 1;
    [SerializeField]
    protected List<Button> menuInGame = null;
    private float vertical = 0;
    private float horizontal = 0;

    public void SelectControllerVertical()
    {
        if (InputManager.Instance.GetAction())
            menuInGame[currentMenuVertical - 1].onClick.Invoke();
        else if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else
            InputControllerVertical();
    }

    private void InputControllerVertical()
    {
        vertical = InputManager.Instance.GetVertical();

        if (vertical == 0)
            return;

        inputController = 0;
        if (vertical > 0)
        {
            currentMenuVertical -= 1;
            if (currentMenuVertical < 1)
                currentMenuVertical = menuInGame.Count;
        }
        else if (vertical < 0)
        {
            currentMenuVertical += 1;
            if (currentMenuVertical > menuInGame.Count)
                currentMenuVertical = 1;
        }
    }

    public void SelectControllerVerticalAndHorizontal(int row = 0, int column = 0)
    {
        if (InputManager.Instance.GetAction())
            menuInGame[(currentMenuHorizontal * currentMenuVertical) - 1].onClick.Invoke();
        else if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else
            InputControllerVerticalAndHorizontal(row, column);
    }

    private void InputControllerVerticalAndHorizontal(int row, int column)
    {
        inputController = 0;

        InputControllerVertical(column);
        InputControllerHorizontal(row);

        ControllerLimitHorizontal(column, row);
    }

    private void InputControllerVertical(int column)
    {
        vertical = InputManager.Instance.GetVertical();

        if (vertical == 0)
            return;

        if (vertical > 0)
        {
            currentMenuVertical -= 1;
            if (currentMenuVertical < 1)
                currentMenuVertical = column;
        }
        else if (vertical < 0)
        {
            currentMenuVertical += 1;
            if (currentMenuVertical > column)
                currentMenuVertical = 1;
        }
    }

    private void InputControllerHorizontal(int row)
    {
        horizontal = InputManager.Instance.GetHorizontal();

        if (horizontal == 0)
            return;

        if (horizontal > 0)
        {
            currentMenuHorizontal += 1;
            if (currentMenuHorizontal > row)
                currentMenuHorizontal = 1;
        }
        else if (horizontal < 0)
        {
            currentMenuHorizontal -= 1;
            if (currentMenuHorizontal < 1)
                currentMenuHorizontal = row;
        }
    }

    private void ControllerLimitHorizontal(int column, int row)
    {
        if (currentMenuVertical < column)
            return;

        int limitLimit = currentMenuVertical * row;
        int mod = limitLimit - menuInGame.Count;
        if (limitLimit > menuInGame.Count && currentMenuHorizontal > (row - mod))
            currentMenuHorizontal = (row - mod);
    }
}
