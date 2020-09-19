using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherManager : MonoBehaviour
{

    public static WeatherManager Instance;

    public WeatherTag currentWeather { get; set; } = WeatherTag.Sunny;

    [Header("Weather Particles")]
    [SerializeField]
    private ParticleSystem snownnyParticle = null;
    [SerializeField]
    private ParticleSystem rainyParticle = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (Instance != null)
            return;

        if (scene.name.Contains("Dungeon"))
            enabled = false;
        else
            enabled = true;
    }

    private void Start()
    {
        StopParticles();
    }

    public void SortWeather()
    {
        StopParticles();
        float randomWeather = Random.value;

        if (randomWeather > 0.9f)
        {
            currentWeather = WeatherTag.Snownny;
            snownnyParticle.Play();
        }
        else if (randomWeather > 0.7f)
        {
            currentWeather = WeatherTag.Rainy;
            rainyParticle.Play();
        }
        else
        {
            currentWeather = WeatherTag.Sunny;
        }
    }

    private void StopParticles()
    {
        snownnyParticle.Stop();
        rainyParticle.Stop();
    }
}
