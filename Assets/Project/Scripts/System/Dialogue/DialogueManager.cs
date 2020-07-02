using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private bool writingType = false;

    [SerializeField]
    private GameObject canvas = null;
    [SerializeField]
    private TextMeshProUGUI nameText = null;
    [SerializeField]
    private TextMeshProUGUI dialogueText = null;

    private Queue<string> sentences = new Queue<string>();

    [SerializeField]
    private float delayNextSentence = 1;
    private float delayNextSentenceController = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        EndDialogue();
    }

    private void Update()
    {
        DelayController();

        if (InputUtil.GetAction() && delayNextSentenceController >= delayNextSentence)
        {
            delayNextSentenceController = 0;
            DisplayNextSentence();
        }
    }

    private void DelayController()
    {
        if (delayNextSentenceController < delayNextSentence)
            delayNextSentenceController += Time.deltaTime;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (sentences.Count > 0)
            return;

        if (nameText)
            nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.GetCurrentDialogueText().sentences)
            sentences.Enqueue(sentence);

        canvas.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (writingType)
            StartCoroutine(TypeSentence(sentence));
        else
            dialogueText.text = sentence;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        canvas.SetActive(false);
        enabled = false;
    }
}
