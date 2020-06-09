using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    new public string name = "";

    public DialogueText[] dialogueTexts = null;
    private DialogueText currentDialogueText;

    public DialogueText GetCurrentDialogueText()
    {
        LanguageTag currentLanguage = LanguageManager.Instance.currentLanguage;

        foreach (DialogueText dialogueText in dialogueTexts)
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
