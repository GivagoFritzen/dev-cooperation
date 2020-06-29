[System.Serializable]
public class EnemyData
{
    public string name = "";
    public int life = 0;
    public float[] position = null;

    public EnemyData(string name, int life, float[] transform)
    {
        this.name = name;
        this.life = life;
        GetPosition(transform);
    }

    private void GetPosition(float[] transform)
    {
        position = new float[3];
        position[0] = transform[0];
        position[1] = transform[1];
        position[2] = transform[2];
    }
}
