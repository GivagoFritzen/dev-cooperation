using UnityEngine;
using TMPro;

public class MenuTextController : MonoBehaviour, IMenuSelectController
{
    private TextMeshProUGUI text = null;
    [SerializeField]
    private Color selected = Color.yellow;
    [SerializeField]
    private Color disabled = Color.white;

    private void Start()
    {
        GetComponent();
    }

    public void SelectFirstOption()
    {
        GetComponent();
        Enable();
    }

    private void GetComponent()
    {
        if (GetComponentInChildren<TextMeshProUGUI>())
            text = GetComponentInChildren<TextMeshProUGUI>();
        else
            Destroy(this);
    }

    public void Enable()
    {
        text.color = selected;
    }

    public void Disable()
    {
        text.color = disabled;
    }
}
