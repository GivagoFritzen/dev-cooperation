using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSpriteController : MonoBehaviour, IMenuSelectController
{
    [Header("Values")]
    [SerializeField]
    private float speed = 1;
    private float inputHorizontal = 0;
    private Slider slider = null;

    [Header("Images")]
    [SerializeField]
    private Image handle = null;
    [SerializeField]
    private Sprite handleSelected = null;
    [SerializeField]
    private Sprite handleDeselected = null;
    private bool isActived = false;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (isActived)
        {
            inputHorizontal = InputManager.Instance.GetHorizontal();

            if (inputHorizontal != 0)
                slider.value += Time.unscaledDeltaTime * inputHorizontal * speed;
        }
    }

    public void Disable()
    {
        handle.sprite = handleDeselected;
        isActived = false;
    }

    public void Enable()
    {
        handle.sprite = handleSelected;
        isActived = true;
    }

    public void SelectFirstOption()
    {
        Enable();
    }
}
