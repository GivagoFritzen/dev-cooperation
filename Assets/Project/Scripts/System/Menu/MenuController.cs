using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IMenuController
{
    [Header("Inputs Controller")]
    [SerializeField]
    protected float delay = 0.15f;
    protected float inputController = 0;
    [SerializeField]
    protected int currentMenuVertical = 1;
    [SerializeField]
    protected int currentMenuHorizontal = 1;
    public List<Button> menuInGame = null;
    protected float vertical = 0;
    protected float horizontal = 0;
    protected int column = 1;
    protected int row = 1;

    [Header("Multiple Screen Controller")]
    protected int currentIndex = 0;
    public List<MenuController> multiplesMenus = new List<MenuController>();
    protected bool spriteController = false;

    public bool isActived { get; set; } = true;

    protected virtual void Init()
    {
        ResetInputController();
        menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<IMenuSelectController>().SelectFirstOption();
    }

    #region Menu Vertical
    protected void SelectControllerVertical()
    {
        if (InputUtil.GetAction())
            menuInGame[currentMenuVertical - 1].onClick.Invoke();
        else if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else
            InputControllerVertical();
    }

    private void InputControllerVertical()
    {
        vertical = InputUtil.GetVertical();

        if (vertical == 0)
            return;

        menuInGame[currentMenuVertical - 1].GetComponent<IMenuSelectController>().Disable();

        ResetInputController();
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

        menuInGame[currentMenuVertical - 1].GetComponent<IMenuSelectController>().Enable();
    }

    protected void ResetInputController()
    {
        inputController = 0;
    }
    #endregion

    #region Menu Vertical and Horizontal
    protected void SelectControllerVerticalAndHorizontal()
    {
        if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else if (InputUtil.GetAction())
            menuInGame[(currentMenuHorizontal * currentMenuVertical) - 1].onClick.Invoke();
        else
            InputControllerVerticalAndHorizontal();
    }

    private void InputControllerVerticalAndHorizontal()
    {
        if (HasMultiplesMenu() && isActived || !HasMultiplesMenu())
        {
            DisableSpriteController();
            InputControllerVerticalRow();
            InputControllerHorizontal();
            ControllerLimitHorizontal();
            menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<IMenuSelectController>().Enable();

            if (vertical != 0 || horizontal != 0)
                ResetInputController();

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
        vertical = InputUtil.GetVertical();

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
        horizontal = InputUtil.GetHorizontal();

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
            currentMenuHorizontal = column - mod;
    }
    #endregion

    #region Multiples Menus
    protected bool HasMultiplesMenu()
    {
        return multiplesMenus != null && multiplesMenus.Count > 0;
    }

    protected void MultiplesMenusController()
    {
        for (int i = 0; i < multiplesMenus.Count; i++)
        {
            if (multiplesMenus[i].isActived)
            {
                multiplesMenus[i].isActived = false;

                if ((i + 1) >= multiplesMenus.Count)
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
    #endregion

    #region Disables Options
    protected void DisableAllMultiplesMenus()
    {
        foreach (var currentMenu in multiplesMenus)
            currentMenu.isActived = false;
    }

    public virtual void DisableSpriteController()
    {
        if (menuInGame != null && menuInGame.Count > 0)
            menuInGame[GetPositionMatrixWithVerticalAndHorizontal()].GetComponent<IMenuSelectController>().Disable();
    }
    #endregion

    #region Get Column and Row Dynamic
    protected void GetColumnAndRowInTheEndOfFrame(GridLayoutGroup gridLayoutGroup)
    {
        StartCoroutine(GetColumnAndRow(gridLayoutGroup));
    }

    private IEnumerator GetColumnAndRow(GridLayoutGroup gridLayoutGroup)
    {
        yield return new WaitForEndOfFrame();
        GridLayoutGroupUtil.GetColumnAndRow(gridLayoutGroup, out column, out row);
    }
    #endregion
}
