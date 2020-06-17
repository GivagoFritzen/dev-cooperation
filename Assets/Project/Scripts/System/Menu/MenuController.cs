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
    [SerializeField]
    protected int column = -1;
    [SerializeField]
    protected int row = -1;

    [Header("Multiple Screen Controller")]
    [SerializeField]
    protected MenuController[] multiplesMenus = null;
    private int currentIndex = 0;
    private bool spriteController = false;

    public bool isActived { get; set; } = true;

    public void SelectControllerVertical()
    {
        if (InputManager.Instance.GetAction())
            menuInGame[currentMenuVertical - 1].onClick.Invoke();
        else if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else
            InputControllerVertical();
    }

    public void Init()
    {
        menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<MenuSpriteController>().SelectFirstOption();
    }

    private void InputControllerVertical()
    {
        vertical = InputManager.Instance.GetVertical();

        if (vertical == 0)
            return;

        menuInGame[currentMenuVertical - 1].GetComponent<MenuSpriteController>().Disable();

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

        menuInGame[currentMenuVertical - 1].GetComponent<MenuSpriteController>().Enable();
    }

    public void SelectControllerVerticalAndHorizontal(int column = 0, int row = 0)
    {
        if (InputManager.Instance.GetAction())
            menuInGame[(currentMenuHorizontal * currentMenuVertical) - 1].onClick.Invoke();
        else if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else
            InputControllerVerticalAndHorizontal();
    }

    private void InputControllerVerticalAndHorizontal()
    {
        inputController = 0;

        if (HasMultiplesMenu() && isActived || !HasMultiplesMenu())
        {
            menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<MenuSpriteController>().Disable();
            InputControllerVerticalRow();
            InputControllerHorizontal();
            ControllerLimitHorizontal();
            menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<MenuSpriteController>().Enable();

            if (spriteController)
            {
                DisableSpriteController();
                spriteController = false;
            }
        }
    }

    private int GetPositionMatrixWithVerticalAndHorizontal()
    {
        int numberOfPreviousColumns = (currentMenuVertical - 1) * column;
        return numberOfPreviousColumns + currentMenuHorizontal - 1;
    }

    private void InputControllerVerticalRow()
    {
        vertical = InputManager.Instance.GetVertical();

        if (vertical == 0)
            return;

        if (vertical > 0)
        {
            currentMenuVertical -= 1;
            if (currentMenuVertical < 1)
                currentMenuVertical = row;
        }
        else if (vertical < 0)
        {
            currentMenuVertical += 1;
            if (currentMenuVertical > row)
                currentMenuVertical = 1;
        }
    }

    private void InputControllerHorizontal()
    {
        horizontal = InputManager.Instance.GetHorizontal();

        if (horizontal == 0)
            return;

        if (horizontal > 0)
        {
            currentMenuHorizontal += 1;
            if (currentMenuHorizontal > column)
            {
                currentMenuHorizontal = 1;

                if (HasMultiplesMenu())
                    MultiplesMenusController();
            }
        }
        else if (horizontal < 0)
        {
            currentMenuHorizontal -= 1;
            if (currentMenuHorizontal < 1)
            {
                currentMenuHorizontal = column;

                if (HasMultiplesMenu())
                    MultiplesMenusController();
            }
        }
    }

    private void ControllerLimitHorizontal()
    {
        if (currentMenuVertical < row)
            return;

        int limitLimit = currentMenuVertical * column;
        int mod = limitLimit - menuInGame.Count;
        if (limitLimit > menuInGame.Count && currentMenuHorizontal > (column - mod))
            currentMenuHorizontal = (column - mod);
    }

    private bool HasMultiplesMenu()
    {
        return multiplesMenus != null && multiplesMenus.Length > 0;
    }

    private void MultiplesMenusController()
    {
        for (int i = 0; i < multiplesMenus.Length; i++)
        {
            if (multiplesMenus[i].isActived)
            {
                multiplesMenus[i].isActived = false;

                if ((i + 1) >= multiplesMenus.Length)
                    currentIndex = 0;
                else
                    currentIndex = i + 1;

                spriteController = true;
                multiplesMenus[currentIndex].isActived = true;
                multiplesMenus[currentIndex].Init();
                break;
            }
        }
    }

    protected void DisableAllMultiplesMenus()
    {
        foreach (var currentMenu in multiplesMenus)
            currentMenu.isActived = false;
    }

    protected void DisableSpriteController()
    {
        menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<MenuSpriteController>().Disable();
    }
}
