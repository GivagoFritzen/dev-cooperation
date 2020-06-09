using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public LanguageTag currentLanguage { get; set; } = LanguageTag.English;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public void ChangeLanguage(LanguageTag newLanguage)
    {
        currentLanguage = newLanguage;
    }
}
