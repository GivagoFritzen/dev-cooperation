using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Text/Dialogue")]
public class Dialogue : ScriptableObject
{
    new public string name = "";

    public TextLanguage[] dialogueTexts = null;
    private TextLanguage currentDialogueText;

    public TextLanguage GetCurrentDialogueText()
    {
        LanguageTag currentLanguage = LanguageManager.Instance.currentLanguage;

        foreach (TextLanguage dialogueText in dialogueTexts)
        {
            if (dialogueText.language == currentLanguage)
            {
                currentDialogueText = dialogueText;
                break;
            }
        }

        return currentDialogueText;
    }
}
