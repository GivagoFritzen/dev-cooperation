using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonMenuSpriteController : MonoBehaviour, IMenuSelectController
{
    [Header("Sprite Controller")]
    private Image image = null;
    [SerializeField]
    private Sprite selected = null;
    [SerializeField]
    private Sprite disabled = null;

    [Header("Text Controller")]
    private TextMeshProUGUI textPro = null;
    private Vector4 originalTextMargin = Vector4.zero;
    [SerializeField]
    private Vector4 selectedTextMargin = Vector4.zero;

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
        if (GetComponent<Image>())
            image = GetComponent<Image>();
        else
            Destroy(this);

        if (GetComponentInChildren<TextMeshProUGUI>())
        {
            textPro = GetComponentInChildren<TextMeshProUGUI>();
            originalTextMargin = textPro.margin;
        }
    }

    public void Enable()
    {
        image.sprite = selected;

        if (textPro != null)
            textPro.margin = selectedTextMargin;
    }

    public void Disable()
    {
        image.sprite = disabled;

        if (textPro != null)
            textPro.margin = originalTextMargin;
    }
}
