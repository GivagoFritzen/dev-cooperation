using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField]
    private string sceneName = null;
    [SerializeField]
    private GameObject transition = null;
    public GameObject exit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScreenAspectRadio screenAspectRadio = Instantiate(transition).GetComponent<ScreenAspectRadio>();
            screenAspectRadio.Init(sceneName);

            SetTransitionPointObject();
        }
    }

    private void SetTransitionPointObject()
    {
        PlayerManager.Instance.transitionPoint = gameObject.name;
    }
}
