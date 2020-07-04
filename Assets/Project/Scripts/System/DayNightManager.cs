using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TextMeshProUGUI dayNightText = null;
    [SerializeField]
    private Light2D globalLight2D = null;

    [Header("Light")]
    [SerializeField]
    private float daySpeed = 1;
    [SerializeField]
    [Range(0, 6)]
    private int dawnTime = 6;
    [SerializeField]
    [Range(18, 24)]
    private int nightfallTime = 18;

    [SerializeField]
    [Range(0, 1)]
    private float maxIntensity = 1;
    [SerializeField]
    [Range(0, 1)]
    private float minIntensity = 0;
    private float intensity = 0;

    public float minutes { get; set; } = 0;
    private int minutesDuration = 60;
    public int hours { get; set; } = 0;
    private int hoursDuration = 24;

    public int day { get; set; } = 1;
    private int dayDuration = 30;

    public int month { get; set; } = 1;
    private int monthDuration = 12;

    public int year { get; set; } = 1;

    private void Update()
    {
        TimerController();
        dayNightText.text = GetText();
        GlobalLightController();
    }

    private string GetText()
    {
        StringBuilder textFormated = new StringBuilder();
        string delimiter = "/";

        int minute = (int)minutes;
        textFormated.Append(minute.ToString());
        textFormated.Append(delimiter);

        textFormated.Append(hours.ToString());
        textFormated.Append(delimiter);

        textFormated.Append(day.ToString());
        textFormated.Append(delimiter);

        textFormated.Append(month.ToString());
        textFormated.Append(delimiter);

        textFormated.Append(year.ToString());
        textFormated.Append(delimiter);

        return textFormated.ToString();
    }

    private void GlobalLightController()
    {
        intensity = globalLight2D.intensity;

        if (IsNight())
            intensity += Time.deltaTime / GetNightfall() * daySpeed;
        else
            intensity -= Time.deltaTime / GetDawn() * daySpeed;

        if (intensity > maxIntensity)
            intensity = maxIntensity;
        else if (intensity < minIntensity)
            intensity = minIntensity;

        globalLight2D.intensity = intensity;
    }

    private bool IsNight()
    {
        if (hours >= nightfallTime)
            return true;
        else
            return false;
    }

    private float GetDawn()
    {
        return dawnTime * minutesDuration;
    }

    private float GetNightfall()
    {
        return (dayDuration - nightfallTime) * minutesDuration;
    }

    private void TimerController()
    {
        MinutesController();
        HoursController();
        DayController();
        MonthController();
    }

    private void MinutesController()
    {
        minutes += Time.deltaTime * daySpeed;
        if (minutes >= minutesDuration)
        {
            minutes = 0;
            hours += 1;
        }
    }

    private void HoursController()
    {
        if (hours >= hoursDuration)
        {
            hours = 0;
            day += 1;
            WeatherManager.Instance.SortWeather();
        }
    }

    private void DayController()
    {
        if (day >= dayDuration)
        {
            day = 1;
            month += 1;
        }
    }

    private void MonthController()
    {
        if (month >= monthDuration)
        {
            month = 1;
            year += 1;
        }
    }
}
