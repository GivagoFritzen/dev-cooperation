using UnityEngine;
using UnityEngine.UI;

public class GridMenuController : MenuController
{
    [SerializeField]
    private GameObject[] columns = null;

    protected override void Init()
    {
        ResetInputController();
        columns[0].GetComponentInChildren<IMenuSelectController>().SelectFirstOption();
    }

    private void Update()
    {
        if (isActived && gameObject.activeSelf)
            SelectControllerVerticalAndHorizontalByColumns();
    }

    protected void SelectControllerVerticalAndHorizontalByColumns()
    {
        if (inputController < delay)
            inputController += Time.unscaledDeltaTime;
        else if (InputUtil.GetAction())
            columns[currentMenuVertical - 1].transform.GetChild(currentMenuHorizontal - 1).GetComponentInChildren<Button>().onClick.Invoke();
        else
            InputControllerVerticalAndHorizontal();
    }

    private void InputControllerVerticalAndHorizontal()
    {
        if (HasMultiplesMenu() && isActived || !HasMultiplesMenu())
        {
            DisableSpriteController();

            InputControllerVertical();
            InputControllerHorizontal();

            columns[currentMenuVertical - 1].transform.GetChild(currentMenuHorizontal - 1).GetComponentInChildren<IMenuSelectController>().Enable();

            if (vertical != 0 || horizontal != 0)
                ResetInputController();

            if (!isActived)
            {
                DisableSpriteController();
                currentMenuHorizontal = currentMenuVertical = 1;
            }
        }
    }

    private void InputControllerVertical()
    {
        vertical = InputUtil.GetVertical();

        if (vertical == 0)
            return;

        if (vertical > 0)
        {
            currentMenuVertical -= 1;
            if (currentMenuVertical < 1)
                currentMenuVertical = columns.Length;
        }
        else if (vertical < 0)
        {
            currentMenuVertical += 1;
            if (currentMenuVertical > columns.Length)
                currentMenuVertical = 1;
        }

        if (vertical != 0 && currentMenuHorizontal > columns[currentMenuVertical - 1].transform.childCount)
            currentMenuHorizontal = columns[currentMenuVertical - 1].transform.childCount;

    }

    private void InputControllerHorizontal()
    {
        horizontal = InputUtil.GetHorizontal();

        if (horizontal == 0)
            return;

        if (horizontal > 0)
        {
            currentMenuHorizontal += 1;
            if (currentMenuHorizontal > columns[currentMenuVertical - 1].transform.childCount)
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
                currentMenuHorizontal = columns[currentMenuVertical - 1].transform.childCount;

                if (HasMultiplesMenu())
                    MultiplesMenusController();
            }
        }
    }

    public override void DisableSpriteController()
    {
        columns[currentMenuVertical - 1].transform.GetChild(currentMenuHorizontal - 1).GetComponentInChildren<IMenuSelectController>().Disable();
    }
}
