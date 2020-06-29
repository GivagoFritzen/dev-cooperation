[System.Serializable]
public class MerchantData
{
    public string name = "";
    public float[] position = null;
    public ItemData[] items = null;

    public MerchantData(string name, ItemData[] items, float[] transform)
    {
        this.name = name;
        this.items = items;
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
