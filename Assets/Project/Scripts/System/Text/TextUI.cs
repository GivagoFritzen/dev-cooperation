using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName = "Text/TextUI")]
public class TextUI : ScriptableObject
{
    public TextLanguage[] texts = null;
    private TextLanguage currentDialogueText;

    public string GetCurrentText()
    {
        LanguageTag currentLanguage = LanguageManager.Instance.currentLanguage;

        foreach (TextLanguage text in texts)
        {
            if (text.language == currentLanguage)
            {
                currentDialogueText = text;
                break;
            }
        }

        return currentDialogueText.sentences[0];
    }
}
