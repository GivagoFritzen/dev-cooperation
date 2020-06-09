using UnityEngine;

[System.Serializable]
public class DialogueText
{
    public LanguageTag language = LanguageTag.English;
    [TextArea(3, 10)]
    public string[] sentences = null;
}
