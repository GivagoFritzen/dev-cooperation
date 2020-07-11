using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField]
    private string sceneName = null;
    [SerializeField]
    private GameObject transition = null;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ScreenAspectRadio screenAspectRadio = Instantiate(transition).GetComponent<ScreenAspectRadio>();
            screenAspectRadio.Init(sceneName);
        }
    }
}
