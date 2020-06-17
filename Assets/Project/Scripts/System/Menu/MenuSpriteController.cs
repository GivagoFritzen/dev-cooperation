using UnityEngine;
using UnityEngine.UI;

public class MenuSpriteController : MonoBehaviour
{
    private Image image = null;
    [SerializeField]
    private Sprite selected = null;
    [SerializeField]
    private Sprite disabled = null;

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
    }

    public void Enable()
    {
        image.sprite = selected;
    }

    public void Disable()
    {
        image.sprite = disabled;
    }
}
