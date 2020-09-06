using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    [Header("Stamina Manager")]
    protected float moveSpeed = 5f;
    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float maxStamina = 100;
    [SerializeField]
    private float stamina = 100;
    [SerializeField]
    private float staminaAddPerSecond = 2;
    [SerializeField]
    private float staminaReductionPerSecond = 2;

    [Header("Stamina UI")]
    [SerializeField]
    private float timeToHide = 2;
    private float timeToHideController = 0;
    [SerializeField]
    private Slider staminaSlider = null;

    public void Init(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;

        GetComponents();
    }

    private void GetComponents()
    {
        staminaSlider = GameObject.Find("Stamina").GetComponent<Slider>();
        staminaSlider.gameObject.SetActive(false);
    }

    public float GetSpeed()
    {
        float speed;

        if (stamina > 0 && InputManager.Instance.GetRun())
        {
            ShowBar();
            DecreaseValue();
            speed = runSpeed;
        }
        else if (stamina < maxStamina)
        {
            IncreaseValue();
            HideBar();
            speed = moveSpeed;
        }
        else
        {
            HideBar();
            speed = moveSpeed;
        }

        return speed;
    }

    private void ShowBar()
    {
        if (!staminaSlider.gameObject.activeSelf)
        {
            staminaSlider.gameObject.SetActive(true);
            timeToHideController = 0;
        }
    }

    private void HideBar()
    {
        timeToHideController += Time.deltaTime;
        if (timeToHideController > timeToHide)
            staminaSlider.gameObject.SetActive(false);
    }

    private void IncreaseValue()
    {
        stamina += Time.deltaTime * staminaAddPerSecond;
        if (stamina > maxStamina)
            stamina = maxStamina;

        staminaSlider.value = stamina;
    }

    private void DecreaseValue()
    {
        stamina -= Time.deltaTime * staminaReductionPerSecond;
        if (stamina < 0)
            stamina = 0;

        staminaSlider.value = stamina;
    }
}
