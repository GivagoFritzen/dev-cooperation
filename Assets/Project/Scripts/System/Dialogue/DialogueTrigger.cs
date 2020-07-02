using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = null;
    private Walker walker = null;

    private void Start()
    {
        if (dialogue == null)
            Destroy(gameObject);

        if (GetComponentInParent<Walker>())
            walker = GetComponentInParent<Walker>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !DialogueManager.Instance.enabled && InputUtil.GetAction())
        {
            StopWalk();
            DialogueManager.Instance.enabled = true;
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartWalk();
            DialogueManager.Instance.EndDialogue();
        }
    }

    private void StartWalk()
    {
        if (walker != null)
            walker.Walk();
    }

    private void StopWalk()
    {
        if (walker != null)
            walker.Stop();
    }
}
