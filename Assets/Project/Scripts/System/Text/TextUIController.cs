using TMPro;
using UnityEngine;

public class TextUIController : MonoBehaviour
{
    private TextMeshProUGUI textPro = null;
    [SerializeField]
    private TextUI text = null;

    private void Start()
    {
        textPro = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText()
    {
        textPro.text = text.GetCurrentText();
    }
}
