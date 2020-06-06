using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = new Dialogue();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && InputManager.Instance.GetAction())
        {
            DialogueManager.Instance.enabled = true;
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            DialogueManager.Instance.EndDialogue();
    }
}
