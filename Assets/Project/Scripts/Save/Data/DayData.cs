[System.Serializable]
public class DayData
{
    public float minutes = 0;
    public int hours = 0;
    public int day = 0;
    public int month = 0;
    public int year = 0;

    public DayData(float minutes, int hours, int day, int month, int year)
    {
        this.minutes = minutes;
        this.hours = hours;
        this.day = day;
        this.month = month;
        this.year = year;
    }
}
