using TMPro;
using UnityEngine;

public class TextUIController : MonoBehaviour
{
    private TextMeshProUGUI textPro = null;
    [SerializeField]
    private TextUI text = null;

    private void Start()
    {
        Resources.Load<TextUI>("ScriptObjects/Rooms/RoomCache/CachedRooms");
        textPro = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText()
    {
        textPro.text = text.GetCurrentText();
    }
}
