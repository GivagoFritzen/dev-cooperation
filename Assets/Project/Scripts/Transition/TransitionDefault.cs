using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionDefault : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1;

    protected async void StartGame(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
