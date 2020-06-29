[System.Serializable]
public class ItemData
{
    public float[] position = null;
    public string name = "";

    public ItemData(Item item, float[] transform)
    {
        name = item ? item.name : "";
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
