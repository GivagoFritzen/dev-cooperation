using System;
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

    public void ChangeLanguage(string newLanguage)
    {
        Enum.TryParse(newLanguage, out LanguageTag language);
        currentLanguage = language;

        UpdateAllTexts();
    }

    public void ChangeLanguage(LanguageTag newLanguage)
    {
        currentLanguage = newLanguage;
        UpdateAllTexts();
    }

    private void UpdateAllTexts()
    {
        foreach (TextUIController text in Resources.FindObjectsOfTypeAll(typeof(TextUIController)) as TextUIController[])
            text.UpdateText();
    }
}
