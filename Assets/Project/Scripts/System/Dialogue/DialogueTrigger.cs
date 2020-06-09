using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = null;

    private void Start()
    {
        if (dialogue == null)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !DialogueManager.Instance.enabled && InputManager.Instance.GetAction())
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
